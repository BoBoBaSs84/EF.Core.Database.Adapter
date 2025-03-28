﻿using BB84.Extensions;
using BB84.Home.Application.Errors.Base;

using RESX = BB84.Home.Application.Properties.ServiceErrors;

namespace BB84.Home.Application.Errors.Services;

/// <summary>
/// The static authentication service errors class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the authentication service.
/// </remarks>
public static class AuthenticationServiceErrors
{
	private const string ErrorPrefix = nameof(AuthenticationServiceErrors);

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	public static readonly ApiError AuthenticateUserFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(AuthenticateUserFailed)}",
			RESX.AuthenticationService_Authenticate_Failed);

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	public static ApiError GetUserByIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetUserByIdFailed)}",
			RESX.AuthenticationService_GetUserById_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	public static ApiError UserByIdNotFound(Guid id)
		=> ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(UserByIdNotFound)}",
			RESX.AuthenticationService_UserById_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	public static ApiError GetUserByNameFailed(string userName)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetUserByNameFailed)}",
			RESX.AuthenticationService_GetUserByName_Failed.FormatInvariant(userName));

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	public static ApiError UserByNameNotFound(string userName)
		=> ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(UserByNameNotFound)}",
			RESX.AuthenticationService_UserByName_NotFound.FormatInvariant(userName));

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	public static ApiError RoleByIdNotFound(Guid id)
		=> ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(RoleByIdNotFound)}",
			RESX.AuthenticationService_RoleById_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	public static ApiError UserUnauthorized(string userName)
		=> ApiError.CreateUnauthorized($"{ErrorPrefix}.{nameof(UserUnauthorized)}",
			RESX.AuthenticationService_User_Unauthorized.FormatInvariant(userName));

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	public static readonly ApiError CreateUserFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateUserFailed)}",
			RESX.AuthenticationService_CreateUser_Failed);

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	public static readonly ApiError CreateUserRolesFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateUserRolesFailed)}",
			RESX.AuthenticationService_CreateUserRoles_Failed);

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	public static readonly ApiError UpdateUserFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(UpdateUserFailed)}",
			RESX.AuthenticationService_UpdateUser_Failed);

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	public static readonly ApiError GetAllFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetAllFailed)}",
			RESX.AuthenticationService_GetAll_Failed);

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	public static readonly ApiError AddUserToRoleFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(AddUserToRoleFailed)}",
			RESX.AuthenticationService_AddUserToRole_Failed);

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	public static readonly ApiError RemoveUserToRoleFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(RemoveUserToRoleFailed)}",
			RESX.AuthenticationService_RemoveUserToRole_Failed);

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	public static readonly ApiError RevokeRefreshTokenFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(RevokeRefreshTokenFailed)}",
			RESX.AuthenticationServiceErrors_RevokeRefreshTokenFailed);

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	public static readonly ApiError RefreshAccessTokenFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(RefreshAccessTokenFailed)}",
			RESX.AuthenticationServiceErrors_RefreshAccessTokenFailed);

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	public static readonly ApiError RefreshAccessTokenVerificationFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(RefreshAccessTokenFailed)}",
			RESX.AuthenticationServiceErrors_RefreshAccessTokenVerificationFailed);
}
