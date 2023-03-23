using Application.Contracts.Requests.Identity;
using Application.Contracts.Responses.Identity;
using Application.Errors.Services;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Identity;
using Application.Interfaces.Infrastructure.Logging;
using AutoMapper;
using Domain.Entities.Identity;
using Domain.Errors;
using Domain.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IC = Infrastructure.Constants.InfrastructureConstants;

namespace Infrastructure.Services;

internal sealed class AuthenticationService : IAuthenticationService
{
	private readonly IConfiguration _configuration;
	private readonly IConfigurationSection _jwtSettings;
	private readonly ILoggerWrapper<AuthenticationService> _logger;
	private readonly IUserService _userService;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, object, Exception?> logExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	/// <summary>
	/// Initilizes an instance of <see cref="AuthenticationService"/> class.
	/// </summary>
	/// <param name="configuration">The configuration.</param>
	/// <param name="logger">The logger service.</param>
	/// <param name="userService">The user service.</param>
	/// <param name="mapper">The auto mapper.</param>
	public AuthenticationService(IConfiguration configuration, ILoggerWrapper<AuthenticationService> logger, IUserService userService, IMapper mapper)
	{
		_configuration = configuration;
		_jwtSettings = _configuration.GetRequiredSection(IC.JwtSettings);
		_logger = logger;
		_userService = userService;
		_mapper = mapper;
	}

	public async Task<ErrorOr<AuthenticationResponse>> Authenticate(AuthenticationRequest authRequest)
	{
		try
		{
			User? user = await _userService.FindByNameAsync(authRequest.UserName);

			if (user is null)
				return AuthenticationServiceErrors.UserByNameNotFound(authRequest.UserName);

			if (!await _userService.CheckPasswordAsync(user, authRequest.Password))
				return AuthenticationServiceErrors.UserUnauthorized(authRequest.UserName);

			SigningCredentials signingCredentials = GetSigningCredentials();
			IEnumerable<Claim> claims = await GetClaims(user);
			JwtSecurityToken tokenOptions = GenerateTokenOptions(signingCredentials, claims);
			string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

			return new AuthenticationResponse() { Token = token };
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, authRequest, ex);
			return AuthenticationServiceErrors.AuthenticateUserFailed;
		}
	}

	// TODO: Error multiple error messages better ...
	public async Task<ErrorOr<Created>> CreateUser(UserCreateRequest createRequest)
	{
		List<Error> errors = new();
		try
		{
			User user = _mapper.Map<User>(createRequest);

			IdentityResult result = await _userService.CreateAsync(user, createRequest.Password);
			if (!result.Succeeded)
			{
				foreach (IdentityError error in result.Errors)					
					errors.Add(AuthenticationServiceErrors.IdentityError(error.Code, error.Description));
				return ErrorOr<Created>.From(errors);
			}

			result = await _userService.AddToRolesAsync(user, createRequest.Roles);
			if (!result.Succeeded)
			{
				foreach (IdentityError error in result.Errors)
					errors.Add(AuthenticationServiceErrors.IdentityError(error.Code, error.Description));
				return ErrorOr<Created>.From(errors);
			}

			return Result.Created;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, createRequest, ex);
			return AuthenticationServiceErrors.CreateUserFailed;
		}
	}

	public async Task<ErrorOr<Updated>> UpdateUser(int userId, UserUpdateRequest updateRequest)
	{
		List<Error> errors = new();
		try
		{
			ErrorOr<Updated> response = new();
			User user = await _userService.FindByIdAsync($"{userId}");

			user.FirstName = updateRequest.FirstName;
			user.MiddleName = updateRequest.MiddleName;
			user.LastName = updateRequest.LastName;
			user.DateOfBirth = updateRequest.DateOfBirth;
			user.Email = updateRequest.Email;
			user.PhoneNumber = updateRequest.PhoneNumber;

			IdentityResult result = await _userService.UpdateAsync(user);
			if (!result.Succeeded)
			{
				foreach (IdentityError error in result.Errors)
					errors.Add(AuthenticationServiceErrors.IdentityError(error.Code, error.Description));
				return response;
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
		byte[] key = Encoding.UTF8.GetBytes(_jwtSettings[IC.SecurityKey]!);

		SymmetricSecurityKey secret = new(key);

		return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
	}

	private async Task<IEnumerable<Claim>> GetClaims(User user)
	{
		IList<Claim> claims = new List<Claim>()
		{
			new(ClaimTypes.Email, user.Email),
			new(ClaimTypes.Name, user.UserName),
			new(ClaimTypes.NameIdentifier, $"{user.Id}"),
			new(ClaimTypes.DateOfBirth, $"{user.DateOfBirth}")
		};

		IList<string> roles = await _userService.GetRolesAsync(user);
		foreach (string role in roles)
			claims.Add(new Claim(ClaimTypes.Role, role));

		return claims;
	}
		

	private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
	{
		JwtSecurityToken tokenOptions = new(
				issuer: _jwtSettings[IC.ValidIssuer],
				audience: _jwtSettings[IC.ValidAudience],
				claims: claims,
				expires: DateTime.Now.AddMinutes(double.Parse(_jwtSettings[IC.ExpiryInMinutes])),
				signingCredentials: signingCredentials);

		return tokenOptions;
	}
}
