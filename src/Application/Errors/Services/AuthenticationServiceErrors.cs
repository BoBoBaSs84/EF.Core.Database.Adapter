using Application.Errors.Base;

using BB84.Extensions;

using RESX = Application.Properties.ServiceErrors;

namespace Application.Errors.Services;

/// <summary>
/// The static authentication service errors class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the authentication service.
/// </remarks>
public static class AuthenticationServiceErrors
{
	private const string ErrorPrefix = $"{nameof(AuthenticationServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	public static readonly ApiError AuthenticateUserFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(AuthenticateUserFailed)}",
			RESX.AuthenticationService_Authenticate_Failed);

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	/// <param name="userId">The user identifier.</param>
	public static ApiError GetUserByIdFailed(Guid userId) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetUserByIdFailed)}",
			RESX.AuthenticationService_GetUserById_Failed.FormatInvariant(userId));

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	/// <param name="userId">The user identifier.</param>
	public static ApiError UserByIdNotFound(Guid userId) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(UserByIdNotFound)}",
			RESX.AuthenticationService_UserById_NotFound.FormatInvariant(userId));

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	/// <param name="userName">The user name.</param>
	public static ApiError GetUserByNameFailed(string userName) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetUserByNameFailed)}",
			RESX.AuthenticationService_GetUserByName_Failed.FormatInvariant(userName));

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	/// <param name="userName">The user name.</param>
	public static ApiError UserByNameNotFound(string userName) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(UserByNameNotFound)}",
			RESX.AuthenticationService_UserByName_NotFound.FormatInvariant(userName));

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	/// <param name="roleId">The role identifier.</param>
	public static ApiError RoleByIdNotFound(Guid roleId) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(RoleByIdNotFound)}",
			RESX.AuthenticationService_RoleById_NotFound.FormatInvariant(roleId));

	/// <summary>
	/// Error that indicates an exception during the authentication service.
	/// </summary>
	/// <param name="userName">The user name.</param>
	public static ApiError UserUnauthorized(string userName) =>
		ApiError.CreateUnauthorized($"{ErrorPrefix}.{nameof(UserUnauthorized)}",
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
	/// <param name="code">The identity error code.</param>
	/// <param name="description">The identity error description.</param>
	public static ApiError IdentityError(string code, string description) =>
		ApiError.CreateBadRequest($"{ErrorPrefix}.{nameof(IdentityError)}",
			$"{code} - {description}");

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
}
