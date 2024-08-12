using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Application.Contracts.Requests.Identity;
using Application.Contracts.Responses.Identity;
using Application.Errors.Services;
using Application.Interfaces.Application.Common;
using Application.Interfaces.Application.Identity;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using BB84.Extensions;

using Domain.Enumerators;
using Domain.Errors;
using Domain.Extensions;
using Domain.Models.Identity;
using Domain.Results;
using Domain.Settings;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Identity;

/// <summary>
/// The authentication service class.
/// </summary>
/// <param name="options">The options for the bearer settings.</param>
/// <param name="dateTimeService">The date time service instance to use.</param>
/// <param name="logger">The logger service instance to use.</param>
/// <param name="roleService">The role service instance to use.</param>
/// <param name="userService">The user service instance to use.</param>
/// <param name="mapper">The auto mapper instance to use.</param>
internal sealed class AuthenticationService(IOptions<BearerSettings> options, IDateTimeService dateTimeService, ILoggerService<AuthenticationService> logger, IRoleService roleService, IUserService userService, IMapper mapper) : IAuthenticationService
{
	private readonly BearerSettings _bearerSettings = options.Value;

	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	public async Task<ErrorOr<Created>> AddUserToRole(Guid userId, Guid roleId)
	{
		try
		{
			UserModel? user = await userService
				.FindByIdAsync($"{userId}")
				.ConfigureAwait(false);

			if (user is null)
				return AuthenticationServiceErrors.UserByIdNotFound(userId);

			RoleModel? role = await roleService
				.FindByIdAsync($"{roleId}")
				.ConfigureAwait(false);

			if (role is null)
				return AuthenticationServiceErrors.RoleByIdNotFound(roleId);

			IdentityResult identityResult = await userService
				.AddToRoleAsync(user, role.Name!)
				.ConfigureAwait(false);

			if (identityResult.Succeeded.IsFalse())
			{
				foreach (IdentityError error in identityResult.Errors)
					logger.Log(LogExceptionWithParams, $"{error.Code} - {error.Description}");

				return AuthenticationServiceErrors.AddUserToRoleFailed;
			}

			return Result.Created;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userId}", $"{roleId}"];
			logger.Log(LogExceptionWithParams, parameters, ex);
			return AuthenticationServiceErrors.AddUserToRoleFailed;
		}
	}

	public async Task<ErrorOr<AuthenticationResponse>> Authenticate(AuthenticationRequest request)
	{
		try
		{
			UserModel? user = await userService.FindByNameAsync(request.UserName);

			if (user is null)
				return AuthenticationServiceErrors.UserUnauthorized(request.UserName);

			if (!await userService.CheckPasswordAsync(user, request.Password))
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
			logger.Log(LogExceptionWithParams, request, ex);
			return AuthenticationServiceErrors.AuthenticateUserFailed;
		}
	}

	public async Task<ErrorOr<Created>> CreateUser(UserCreateRequest request)
	{
		try
		{
			UserModel user = mapper.Map<UserModel>(request);

			IdentityResult result = await userService
				.CreateAsync(user, request.Password)
				.ConfigureAwait(false);

			if (result.Succeeded.IsFalse())
			{
				foreach (IdentityError error in result.Errors)
					logger.Log(LogExceptionWithParams, $"{error.Code} - {error.Description}");

				return AuthenticationServiceErrors.CreateUserFailed;
			}

			result = await userService
				.AddToRoleAsync(user, RoleType.USER.GetName())
				.ConfigureAwait(false);

			if (result.Succeeded.IsFalse())
			{
				foreach (IdentityError error in result.Errors)
					logger.Log(LogExceptionWithParams, $"{error.Code} - {error.Description}");

				return AuthenticationServiceErrors.AddUserToRoleFailed;
			}

			return Result.Created;
		}
		catch (Exception ex)
		{
			logger.Log(LogExceptionWithParams, request, ex);
			return AuthenticationServiceErrors.CreateUserFailed;
		}
	}

	public async Task<ErrorOr<IEnumerable<UserResponse>>> GetAllUser()
	{
		try
		{
			IList<UserModel> userEntities = await userService
				.GetUsersInRoleAsync(RoleType.USER.GetName())
				.ConfigureAwait(false);

			IEnumerable<UserResponse> response = mapper.Map<IEnumerable<UserResponse>>(userEntities);

			return response.ToList();
		}
		catch (Exception ex)
		{
			logger.Log(LogException, ex);
			return AuthenticationServiceErrors.GetAllFailed;
		}
	}

	public async Task<ErrorOr<UserResponse>> GetUserById(Guid userId)
	{
		try
		{
			UserModel? user = await userService.FindByIdAsync($"{userId}");

			if (user is null)
				return AuthenticationServiceErrors.UserByIdNotFound(userId);

			UserResponse response =
				mapper.Map<UserResponse>(user);

			return response;
		}
		catch (Exception ex)
		{
			logger.Log(LogExceptionWithParams, userId, ex);
			return AuthenticationServiceErrors.GetUserByIdFailed(userId);
		}
	}

	public async Task<ErrorOr<UserResponse>> GetUserByName(string userName)
	{
		try
		{
			UserModel? user = await userService.FindByNameAsync(userName);

			if (user is null)
				return AuthenticationServiceErrors.UserByNameNotFound(userName);

			UserResponse response = mapper.Map<UserResponse>(user);

			return response;
		}
		catch (Exception ex)
		{
			logger.Log(LogExceptionWithParams, userName, ex);
			return AuthenticationServiceErrors.GetUserByNameFailed(userName);
		}
	}

	public async Task<ErrorOr<Deleted>> RemoveUserFromRole(Guid userId, Guid roleId)
	{
		try
		{
			UserModel? user = await userService.FindByIdAsync($"{userId}");

			if (user is null)
				return AuthenticationServiceErrors.UserByIdNotFound(userId);

			RoleModel? role = await roleService.FindByIdAsync($"{roleId}");

			if (role is null)
				return AuthenticationServiceErrors.RoleByIdNotFound(roleId);

			IdentityResult result = await userService.RemoveFromRoleAsync(user, role.Name!);

			if (result.Succeeded.IsFalse())
			{
				foreach (IdentityError error in result.Errors)
					logger.Log(LogExceptionWithParams, $"{error.Code} - {error.Description}");

				return AuthenticationServiceErrors.RemoveUserToRoleFailed;
			}

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userId}", $"{roleId}"];
			logger.Log(LogExceptionWithParams, parameters, ex);
			return AuthenticationServiceErrors.RemoveUserToRoleFailed;
		}
	}

	public async Task<ErrorOr<Updated>> UpdateUser(Guid userId, UserUpdateRequest request)
	{
		try
		{
			UserModel? user = await userService
				.FindByIdAsync($"{userId}")
				.ConfigureAwait(false);

			if (user is null)
				return AuthenticationServiceErrors.UserByIdNotFound(userId);

			user.FirstName = request.FirstName;
			user.MiddleName = request.MiddleName;
			user.LastName = request.LastName;
			user.DateOfBirth = request.DateOfBirth;
			user.Email = request.Email;
			user.PhoneNumber = request.PhoneNumber;
			user.Picture = Convert.FromBase64String(request.Picture ?? string.Empty);
			user.Preferences = mapper.Map<PreferencesModel>(request.Preferences);

			IdentityResult result = await userService
				.UpdateAsync(user)
				.ConfigureAwait(false);

			if (result.Succeeded.IsFalse())
			{
				foreach (IdentityError error in result.Errors)
					logger.Log(LogExceptionWithParams, $"{error.Code} - {error.Description}");

				return AuthenticationServiceErrors.UpdateUserFailed;
			}

			return Result.Updated;
		}
		catch (Exception ex)
		{
			logger.Log(LogExceptionWithParams, request, ex);
			return AuthenticationServiceErrors.UpdateUserFailed;
		}
	}

	private SigningCredentials GetSigningCredentials()
	{
		byte[] key = Encoding.UTF8.GetBytes(_bearerSettings.SecurityKey);
		SymmetricSecurityKey secret = new(key);
		return new SigningCredentials(secret, SecurityAlgorithms.HmacSha512);
	}

	private async Task<IEnumerable<Claim>> GetClaims(UserModel user)
	{
		List<Claim> claims = [
			new(ClaimTypes.DateOfBirth, $"{user.DateOfBirth}"),
			new(ClaimTypes.Email, $"{user.Email}"),
			new(ClaimTypes.GivenName, $"{user.LastName}, {user.FirstName}"),
			new(ClaimTypes.MobilePhone, $"{user.PhoneNumber}"),
			new(ClaimTypes.Name, $"{user.UserName}"),
			new(ClaimTypes.NameIdentifier, $"{user.Id}")
			];

		IList<string> roles = await userService.GetRolesAsync(user);

		foreach (string role in roles)
			claims.Add(new Claim(ClaimTypes.Role, role));

		return claims;
	}

	private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
	{
		JwtSecurityToken tokenOptions = new(
				issuer: _bearerSettings.Issuer,
				audience: _bearerSettings.Audience,
				claims: claims,
				expires: dateTimeService.Now.AddMinutes(_bearerSettings.ExpiryInMinutes),
				signingCredentials: signingCredentials);

		return tokenOptions;
	}
}
