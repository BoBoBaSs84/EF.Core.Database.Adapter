using Application.Contracts.Requests.Identity;
using Application.Contracts.Responses.Identity;
using Application.Errors.Base;
using Application.Errors.Services;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Services;
using AutoMapper;
using Domain.Entities.Identity;
using Domain.Errors;
using Domain.Extensions;
using Domain.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Jwt = Infrastructure.Constants.InfrastructureConstants.BearerJwt;
using Roles = Domain.Enumerators.RoleTypes;

namespace Infrastructure.Services;

internal sealed class AuthenticationService : IAuthenticationService
{
	private readonly IConfiguration _configuration;
	private readonly IConfigurationSection _jwtSettings;
	private readonly ILoggerWrapper<AuthenticationService> _logger;
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
	/// <param name="logger">The logger service.</param>
	/// <param name="roleService">The role service.</param>
	/// <param name="userService">The user service.</param>
	/// <param name="mapper">The auto mapper.</param>
	public AuthenticationService(
		IConfiguration configuration,
		ILoggerWrapper<AuthenticationService> logger,
		IRoleService roleService,
		IUserService userService,
		IMapper mapper
		)
	{
		_configuration = configuration;
		_jwtSettings = _configuration.GetRequiredSection(Jwt.JwtSettings);
		_logger = logger;
		_roleService = roleService;
		_userService = userService;
		_mapper = mapper;
	}

	public async Task<ErrorOr<Created>> AddUserToRole(int userId, string roleName)
	{
		string[] parameters = new[] { $"{userId}", $"{roleName}" };
		ErrorOr<Created> response = new();
		try
		{
			User user = await _userService.FindByIdAsync($"{userId}");

			if (user is null)
				return AuthenticationServiceErrors.UserByIdNotFound(userId);

			Role role = await _roleService.FindByNameAsync(roleName);

			if (role is null)
				return AuthenticationServiceErrors.RoleByNameNotFound(roleName);

			IdentityResult identityResult = await _userService.AddToRoleAsync(user, roleName);

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
			// TODO: Error
			_logger.Log(logExceptionWithParams, parameters, ex);
			return ApiError.CreateFailed("", "");
		}
	}

	public async Task<ErrorOr<AuthenticationResponse>> Authenticate(AuthenticationRequest authRequest)
	{
		try
		{
			User? user = await _userService.FindByNameAsync(authRequest.UserName);

			if (user is null)
				return AuthenticationServiceErrors.UserUnauthorized(authRequest.UserName);

			if (!await _userService.CheckPasswordAsync(user, authRequest.Password))
				return AuthenticationServiceErrors.UserUnauthorized(authRequest.UserName);

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
			_logger.Log(logExceptionWithParams, authRequest, ex);
			return AuthenticationServiceErrors.AuthenticateUserFailed;
		}
	}

	public async Task<ErrorOr<Created>> CreateUser(UserCreateRequest createRequest)
	{
		ErrorOr<Created> response = new();
		try
		{
			User user = _mapper.Map<User>(createRequest);

			IdentityResult result = await _userService.CreateAsync(user, createRequest.Password);

			if (!result.Succeeded)
			{
				foreach (IdentityError error in result.Errors)
					response.Errors.Add(AuthenticationServiceErrors.IdentityError(error.Code, error.Description));
				return response;
			}

			result = await _userService.AddToRolesAsync(user, new[] { Roles.USER.GetName() });

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
			_logger.Log(logExceptionWithParams, createRequest, ex);
			return AuthenticationServiceErrors.CreateUserFailed;
		}
	}

	public async Task<ErrorOr<IEnumerable<UserResponse>>> GetAll()
	{
		try
		{
			IList<User> users = await _userService.GetUsersInRoleAsync(Roles.USER.GetName());

			IEnumerable<UserResponse> response = _mapper.Map<IEnumerable<UserResponse>>(users);

			return response.ToList();
		}
		catch (Exception ex)
		{
			_logger.Log(logException, ex);
			return AuthenticationServiceErrors.GetAllFailed;
		}
	}

	public async Task<ErrorOr<UserResponse>> GetUserById(int userId)
	{
		try
		{
			User user = await _userService.FindByIdAsync($"{userId}");

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
			User user = await _userService.FindByNameAsync(userName);

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

	public async Task<ErrorOr<Deleted>> RemoveUserToRole(int userId, string roleName)
	{
		string[] parameters = new[] { $"{userId}", $"{roleName}" };
		ErrorOr<Deleted> response = new();

		try
		{
			User user = await _userService.FindByIdAsync($"{userId}");

			if (user is null)
				return AuthenticationServiceErrors.UserByIdNotFound(userId);

			Role role = await _roleService.FindByNameAsync(roleName);

			if (role is null)
				return AuthenticationServiceErrors.RoleByNameNotFound(roleName);

			IdentityResult identityResult = await _userService.RemoveFromRoleAsync(user, roleName);

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

	public async Task<ErrorOr<Updated>> UpdateUser(int userId, UserUpdateRequest updateRequest)
	{
		ErrorOr<Updated> response = new();
		try
		{
			User user = await _userService.FindByIdAsync($"{userId}");

			user.FirstName = updateRequest.FirstName;
			user.MiddleName = updateRequest.MiddleName;
			user.LastName = updateRequest.LastName;
			user.DateOfBirth = updateRequest.DateOfBirth;
			user.Email = updateRequest.Email;
			user.PhoneNumber = updateRequest.PhoneNumber;
			user.Picture = updateRequest.Picture;

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
			_logger.Log(logExceptionWithParams, updateRequest, ex);
			return AuthenticationServiceErrors.UpdateUserFailed;
		}
	}

	private SigningCredentials GetSigningCredentials()
	{
		byte[] key = Encoding.UTF8.GetBytes(_jwtSettings[Jwt.SecurityKey]!);
		SymmetricSecurityKey secret = new(key);
		return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
	}

	private async Task<IEnumerable<Claim>> GetClaims(User user)
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
				expires: DateTime.Now.AddMinutes(int.Parse(expiryInMinutes, CultureInfo.CurrentCulture)),
				signingCredentials: signingCredentials);

		return tokenOptions;
	}
}
