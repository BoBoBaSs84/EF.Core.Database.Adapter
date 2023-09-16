using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Application.Contracts.Requests.Identity;
using Application.Contracts.Responses.Identity;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using Domain.Models.Identity;
using Domain.Enumerators;
using Domain.Errors;
using Domain.Extensions;
using Domain.Results;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using Jwt = Infrastructure.Constants.InfrastructureConstants.BearerJwt;

namespace Infrastructure.Services;

internal sealed class AuthenticationService : IAuthenticationService
{
	private readonly IConfiguration _configuration;
	private readonly IConfigurationSection _jwtSettings;
	private readonly IDateTimeService _dateTimeService;
	private readonly ILoggerService<AuthenticationService> _logger;
	private readonly IRoleService _roleService;
	private readonly IUserService _userService;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, object, Exception?> logExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	private static readonly Action<ILogger, Exception?> logException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	/// <summary>
	/// Initilizes an instance of <see cref="AuthenticationService"/> class.
	/// </summary>
	/// <param name="configuration">The configuration.</param>
	/// <param name="dateTimeService">The date time service.</param>
	/// <param name="logger">The logger service.</param>
	/// <param name="roleService">The role service.</param>
	/// <param name="userService">The user service.</param>
	/// <param name="mapper">The auto mapper.</param>
	public AuthenticationService(
		IConfiguration configuration,
		IDateTimeService dateTimeService,
		ILoggerService<AuthenticationService> logger,
		IRoleService roleService,
		IUserService userService,
		IMapper mapper
		)
	{
		_configuration = configuration;
		_jwtSettings = _configuration.GetRequiredSection(Jwt.JwtSettings);
		_dateTimeService = dateTimeService;
		_logger = logger;
		_roleService = roleService;
		_userService = userService;
		_mapper = mapper;
	}

	public async Task<ErrorOr<Created>> AddUserToRole(Guid userId, Guid roleId)
	{
		string[] parameters = new[] { $"{userId}", $"{roleId}" };
		ErrorOr<Created> response = new();
		try
		{
			UserModel user = await _userService.FindByIdAsync($"{userId}");

			if (user is null)
				return AuthenticationServiceErrors.UserByIdNotFound(userId);

			RoleModel role = await _roleService.FindByIdAsync($"{roleId}");

			if (role is null)
				return AuthenticationServiceErrors.RoleByIdNotFound(roleId);

			IdentityResult identityResult = await _userService.AddToRoleAsync(user, role.Name);

			if (!identityResult.Succeeded)
			{
				foreach (IdentityError error in identityResult.Errors)
					response.Errors.Add(AuthenticationServiceErrors.IdentityError($"{error.Code}", $"{error.Description}"));
				return response;
			}

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, parameters, ex);
			return AuthenticationServiceErrors.AddUserToRoleFailed;
		}
	}

	public async Task<ErrorOr<AuthenticationResponse>> Authenticate(AuthenticationRequest request)
	{
		try
		{
			UserModel? user = await _userService.FindByNameAsync(request.UserName);

			if (user is null)
				return AuthenticationServiceErrors.UserUnauthorized(request.UserName);

			if (!await _userService.CheckPasswordAsync(user, request.Password))
				return AuthenticationServiceErrors.UserUnauthorized(request.UserName);

			SigningCredentials signingCredentials = GetSigningCredentials();
			IEnumerable<Claim> claims = await GetClaims(user);
			JwtSecurityToken tokenOptions = GenerateTokenOptions(signingCredentials, claims);
			string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

			AuthenticationResponse response = new()
			{
				Token = token,
				ExpiryDate = tokenOptions.ValidTo
			};

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, request, ex);
			return AuthenticationServiceErrors.AuthenticateUserFailed;
		}
	}

	public async Task<ErrorOr<Created>> CreateUser(UserCreateRequest request)
	{
		ErrorOr<Created> response = new();
		try
		{
			UserModel user = _mapper.Map<UserModel>(request);

			IdentityResult result = await _userService.CreateAsync(user, request.Password);

			if (!result.Succeeded)
			{
				foreach (IdentityError error in result.Errors)
					response.Errors.Add(AuthenticationServiceErrors.IdentityError(error.Code, error.Description));
				return response;
			}

			result = await _userService.AddToRolesAsync(user, new[] { RoleType.USER.GetName() });

			if (!result.Succeeded)
			{
				foreach (IdentityError error in result.Errors)
					response.Errors.Add(AuthenticationServiceErrors.IdentityError(error.Code, error.Description));
				return response;
			}

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, request, ex);
			return AuthenticationServiceErrors.CreateUserFailed;
		}
	}

	public async Task<ErrorOr<IEnumerable<UserResponse>>> GetAll()
	{
		try
		{
			IList<UserModel> users = await _userService.GetUsersInRoleAsync(RoleType.USER.GetName());

			IEnumerable<UserResponse> response = _mapper.Map<IEnumerable<UserResponse>>(users);

			return response.ToList();
		}
		catch (Exception ex)
		{
			_logger.Log(logException, ex);
			return AuthenticationServiceErrors.GetAllFailed;
		}
	}

	public async Task<ErrorOr<UserResponse>> GetUserById(Guid userId)
	{
		try
		{
			UserModel user = await _userService.FindByIdAsync($"{userId}");

			if (user is null)
				return AuthenticationServiceErrors.UserByIdNotFound(userId);

			UserResponse response = _mapper.Map<UserResponse>(user);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, userId, ex);
			return AuthenticationServiceErrors.GetUserByIdFailed(userId);
		}
	}

	public async Task<ErrorOr<UserResponse>> GetUserByName(string userName)
	{
		try
		{
			UserModel user = await _userService.FindByNameAsync(userName);

			if (user is null)
				return AuthenticationServiceErrors.UserByNameNotFound(userName);

			UserResponse response = _mapper.Map<UserResponse>(user);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, userName, ex);
			return AuthenticationServiceErrors.GetUserByNameFailed(userName);
		}
	}

	public async Task<ErrorOr<Deleted>> RemoveUserFromRole(Guid userId, Guid roleId)
	{
		string[] parameters = new[] { $"{userId}", $"{roleId}" };
		ErrorOr<Deleted> response = new();

		try
		{
			UserModel user = await _userService.FindByIdAsync($"{userId}");

			if (user is null)
				return AuthenticationServiceErrors.UserByIdNotFound(userId);

			RoleModel role = await _roleService.FindByIdAsync($"{roleId}");

			if (role is null)
				return AuthenticationServiceErrors.RoleByIdNotFound(roleId);

			IdentityResult identityResult = await _userService.RemoveFromRoleAsync(user, role.Name);

			if (!identityResult.Succeeded)
			{
				foreach (IdentityError error in identityResult.Errors)
					response.Errors.Add(AuthenticationServiceErrors.IdentityError($"{error.Code}", $"{error.Description}"));
				return response;
			}

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, parameters, ex);
			return AuthenticationServiceErrors.RemoveUserToRoleFailed;
		}
	}

	public async Task<ErrorOr<Updated>> UpdateUser(Guid userId, UserUpdateRequest request)
	{
		ErrorOr<Updated> response = new();
		try
		{
			UserModel user = await _userService.FindByIdAsync($"{userId}");

			if (user is null)
				return AuthenticationServiceErrors.UserByIdNotFound(userId);

			user.FirstName = request.FirstName;
			user.MiddleName = request.MiddleName;
			user.LastName = request.LastName;
			user.Email = request.Email;
			user.PhoneNumber = request.PhoneNumber;
			user.Picture = Convert.FromBase64String(request.Picture ?? string.Empty);
			user.Preferences = _mapper.Map<PreferencesModel>(request.Preferences);

			IdentityResult result = await _userService.UpdateAsync(user);

			if (!result.Succeeded)
			{
				foreach (IdentityError error in result.Errors)
					response.Errors.Add(AuthenticationServiceErrors.IdentityError(error.Code, error.Description));
			}

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, request, ex);
			return AuthenticationServiceErrors.UpdateUserFailed;
		}
	}

	private SigningCredentials GetSigningCredentials()
	{
		byte[] key = Encoding.UTF8.GetBytes(_jwtSettings[Jwt.SecurityKey]!);
		SymmetricSecurityKey secret = new(key);
		return new SigningCredentials(secret, SecurityAlgorithms.HmacSha512);
	}

	private async Task<IEnumerable<Claim>> GetClaims(UserModel user)
	{
		IList<Claim> claims = new List<Claim>()
		{
			new(ClaimTypes.Name, user.UserName),
			new(ClaimTypes.NameIdentifier, $"{user.Id}"),
		};

		IList<string> roles = await _userService.GetRolesAsync(user);
		foreach (string role in roles)
			claims.Add(new Claim(ClaimTypes.Role, role));

		return claims;
	}

	private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
	{
		string expiryInMinutes = _jwtSettings[Jwt.ExpiryInMinutes] ?? "5";

		JwtSecurityToken tokenOptions = new(
				issuer: _jwtSettings[Jwt.ValidIssuer],
				audience: _jwtSettings[Jwt.ValidAudience],
				claims: claims,
				expires: _dateTimeService.Now.AddMinutes(int.Parse(expiryInMinutes, CultureInfo.CurrentCulture)),
				signingCredentials: signingCredentials);

		return tokenOptions;
	}
}
