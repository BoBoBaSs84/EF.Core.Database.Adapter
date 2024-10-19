using System.Security.Claims;

using Application.Contracts.Requests.Identity;
using Application.Contracts.Responses.Identity;
using Application.Errors.Services;
using Application.Interfaces.Application.Identity;
using Application.Interfaces.Infrastructure.Services;
using Application.Options;

using AutoMapper;

using BB84.Extensions;

using Domain.Enumerators;
using Domain.Errors;
using Domain.Extensions;
using Domain.Models.Identity;
using Domain.Results;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Services.Identity;

/// <summary>
/// The authentication service class.
/// </summary>
/// <param name="logger">The logger service instance to use.</param>
/// <param name="options">The bearer option instance to use.</param>
/// <param name="tokenService">The token service instance to use.</param>
/// <param name="roleService">The role service instance to use.</param>
/// <param name="userService">The user service instance to use.</param>
/// <param name="mapper">The auto mapper instance to use.</param>
internal sealed class AuthenticationService(ILoggerService<AuthenticationService> logger, IOptions<BearerSettings> options, ITokenService tokenService, IRoleService roleService, IUserService userService, IMapper mapper) : IAuthenticationService
{
	private const string RefreshTokenName = "RefreshToken";
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
			UserModel? user = await userService
				.FindByNameAsync(request.UserName)
				.ConfigureAwait(false);

			if (user is null)
				return AuthenticationServiceErrors.UserUnauthorized(request.UserName);

			bool success = await userService
				.CheckPasswordAsync(user, request.Password)
				.ConfigureAwait(false);

			if (success.IsFalse())
				return AuthenticationServiceErrors.UserUnauthorized(request.UserName);

			IList<string> roles = await userService
				.GetRolesAsync(user)
				.ConfigureAwait(false);

			List<Claim> claims = GetClaims(user, roles);
			string token = tokenService.GenerateAccessToken(claims);

			string refreshToken = await userService
				.GenerateUserTokenAsync(user, _bearerSettings.Issuer, RefreshTokenName)
				.ConfigureAwait(false);

			IdentityResult result = await userService
				.SetAuthenticationTokenAsync(user, _bearerSettings.Issuer, RefreshTokenName, refreshToken)
				.ConfigureAwait(false);

			AuthenticationResponse response = new()
			{
				AccessToken = token,
				RefreshToken = refreshToken
			};

			return response;
		}
		catch (Exception ex)
		{
			logger.Log(LogException, ex);
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
			UserModel? user = await userService.FindByIdAsync($"{userId}")
				.ConfigureAwait(false); ;

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
			UserModel? user = await userService.FindByNameAsync(userName)
				.ConfigureAwait(false);

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
			UserModel? user = await userService.FindByIdAsync($"{userId}")
				.ConfigureAwait(false); ;

			if (user is null)
				return AuthenticationServiceErrors.UserByIdNotFound(userId);

			RoleModel? role = await roleService.FindByIdAsync($"{roleId}")
				.ConfigureAwait(false); ;

			if (role is null)
				return AuthenticationServiceErrors.RoleByIdNotFound(roleId);

			IdentityResult result = await userService.RemoveFromRoleAsync(user, role.Name!)
				.ConfigureAwait(false); ;

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

	public async Task<ErrorOr<AuthenticationResponse>> RefreshToken(TokenRequest request)
	{
		try
		{
			ClaimsPrincipal principal = tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
			string? userName = principal.Identity?.Name;

			if (userName is null)
				// TODO
				throw new Exception();

			UserModel? user = await userService.FindByNameAsync(userName)
				.ConfigureAwait(false);

			if (user is null)
				// TODO
				throw new Exception();

			bool result = await userService
				.VerifyUserTokenAsync(user, _bearerSettings.Issuer, RefreshTokenName, request.RefreshToken)
				.ConfigureAwait(false);

			if (result.IsFalse())
				// TODO
				throw new Exception();

			string newAccessToken = tokenService.GenerateAccessToken(principal.Claims);
			string newRefreshToken = await userService.GenerateUserTokenAsync(user, _bearerSettings.Issuer, RefreshTokenName);

			AuthenticationResponse response = new()
			{
				AccessToken = newAccessToken,
				RefreshToken = newRefreshToken
			};

			return response;
		}
		catch (Exception ex)
		{
			logger.Log(LogException, ex);
			// TODO
			throw new Exception();
		}
	}

	public async Task<ErrorOr<VoidResult>> RevokeToken(Guid userId)
	{
		try
		{
			UserModel? user = await userService.FindByIdAsync($"{userId}")
				.ConfigureAwait(false);

			if (user is null)
				return AuthenticationServiceErrors.UserByIdNotFound(userId);

			IdentityResult result = await userService
				.RemoveAuthenticationTokenAsync(user, _bearerSettings.Issuer, RefreshTokenName)
				.ConfigureAwait(false);

			if (result.Succeeded.IsFalse())
				// TODO
				throw new Exception();

			return Result.Void;
		}
		catch (Exception ex)
		{
			logger.Log(LogException, ex);
			// TODO
			return Result.Void;
		}
	}

	public async Task<ErrorOr<Updated>> UpdateUser(Guid userId, UserUpdateRequest request)
	{
		try
		{
			UserModel? user = await userService.FindByIdAsync($"{userId}")
				.ConfigureAwait(false);

			if (user is null)
				return AuthenticationServiceErrors.UserByIdNotFound(userId);

			_ = mapper.Map(request, user);

			//user.FirstName = request.FirstName;
			//user.MiddleName = request.MiddleName;
			//user.LastName = request.LastName;
			//user.DateOfBirth = request.DateOfBirth;
			//user.Email = request.Email;
			//user.PhoneNumber = request.PhoneNumber;
			//user.Picture = request.Picture;
			//user.Preferences = mapper.Map<PreferencesModel>(request.Preferences);

			IdentityResult result = await userService.UpdateAsync(user)
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

	private static List<Claim> GetClaims(UserModel user, IList<string> roles)
	{
		List<Claim> claims = [
			new(ClaimTypes.NameIdentifier, $"{user.Id}"),
			new(ClaimTypes.Name, $"{user.UserName}"),			
			new(ClaimTypes.Email, $"{user.Email}"),
			new(ClaimTypes.GivenName, $"{user.FirstName}"),
			new(ClaimTypes.Surname, $"{user.LastName}"),			
			new(ClaimTypes.MobilePhone, $"{user.PhoneNumber}"),
			new(ClaimTypes.DateOfBirth, $"{user.DateOfBirth}")
			];

		foreach (string role in roles)
			claims.Add(new Claim(ClaimTypes.Role, role));

		return claims;
	}
}
