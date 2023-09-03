using System.Globalization;

using Application.Errors.Base;

using Domain.Extensions;

using RESX = Application.Properties.ServiceErrors;

namespace Application.Errors.Services;

/// <summary>
/// The static <see cref="AuthenticationServiceErrors"/> class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the authentication service.
/// </remarks>
public static class AuthenticationServiceErrors
{
	private static readonly CultureInfo CurrentCulture = CultureInfo.CurrentCulture;
	private const string ErrorPrefix = $"{nameof(AuthenticationServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the user authentication.
	/// </summary>
	public static readonly ApiError AuthenticateUserFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(AuthenticateUserFailed)}",
			RESX.AuthenticationService_Authenticate_Failed);

	/// <summary>
	/// Error that indicates an exception during the user authentication.
	/// </summary>
	/// <param name="userId">The user identifier.</param>
	/// <returns><see cref="ApiError"/></returns>
	public static ApiError GetUserByIdFailed(Guid userId) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetUserByIdFailed)}",
			RESX.AuthenticationService_GetUserById_Failed.Format(CurrentCulture, userId));

	/// <summary>
	/// Error that indicates an exception during the user authentication.
	/// </summary>
	/// <param name="userId">The user identifier.</param>
	/// <returns><see cref="ApiError"/></returns>
	public static ApiError UserByIdNotFound(Guid userId) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(UserByIdNotFound)}",
			RESX.AuthenticationService_UserById_NotFound.Format(CurrentCulture, userId));

	/// <summary>
	/// Error that indicates an exception during the user authentication.
	/// </summary>
	/// <param name="userName">The user name.</param>
	/// <returns><see cref="ApiError"/></returns>
	public static ApiError GetUserByNameFailed(string userName) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetUserByNameFailed)}",
			RESX.AuthenticationService_GetUserByName_Failed.Format(CurrentCulture, userName));

	/// <summary>
	/// Error that indicates an exception during the user authentication.
	/// </summary>
	/// <param name="userName">The user name.</param>
	/// <returns><see cref="ApiError"/></returns>
	public static ApiError UserByNameNotFound(string userName) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(UserByNameNotFound)}",
			RESX.AuthenticationService_UserByName_NotFound.Format(CurrentCulture, userName));

	/// <summary>
	/// Error that indicates an exception during the user authentication.
	/// </summary>
	/// <param name="roleName">The user name.</param>
	/// <returns><see cref="ApiError"/></returns>
	public static ApiError RoleByNameNotFound(string roleName) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(RoleByNameNotFound)}",
			RESX.AuthenticationService_RoleByName_NotFound.Format(CurrentCulture, roleName));

	/// <summary>
	/// Error that indicates an exception during the user authentication.
	/// </summary>
	/// <param name="userName">The user name.</param>
	/// <returns><see cref="ApiError"/></returns>
	public static ApiError UserUnauthorized(string userName) =>
		ApiError.CreateUnauthorized($"{ErrorPrefix}.{nameof(UserUnauthorized)}",
			RESX.AuthenticationService_User_Unauthorized.Format(CurrentCulture, userName));

	/// <summary>
	/// Error that indicates an exception during the user creation.
	/// </summary>
	public static readonly ApiError CreateUserFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateUserFailed)}",
			RESX.AuthenticationService_CreateUser_Failed);

	/// <summary>
	/// Error that indicates an exception during the user creation.
	/// </summary>
	public static readonly ApiError CreateUserRolesFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateUserRolesFailed)}",
			RESX.AuthenticationService_CreateUserRoles_Failed);

	/// <summary>
	/// Error that indicates an exception during the user update.
	/// </summary>
	public static readonly ApiError UpdateUserFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(UpdateUserFailed)}",
			RESX.AuthenticationService_UpdateUser_Failed);

	/// <summary>
	/// Error that indicates an exception during the identity opertations.
	/// </summary>
	/// <param name="code">The identity error code.</param>
	/// <param name="description">The identity error description.</param>
	public static ApiError IdentityError(string code, string description) =>
		ApiError.CreateBadRequest($"{ErrorPrefix}.{nameof(IdentityError)}",
			$"{code} - {description}");

	/// <summary>
	/// Error that indicates an exception during the user authentication.
	/// </summary>
	public static readonly ApiError GetAllFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetAllFailed)}",
			RESX.AuthenticationService_GetAll_Failed);

	/// <summary>
	/// Error that indicates an exception during the user authentication.
	/// </summary>
	public static readonly ApiError AddUserToRoleFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(AddUserToRoleFailed)}",
			RESX.AuthenticationService_AddUserToRole_Failed);

	/// <summary>
	/// Error that indicates an exception during the user authentication.
	/// </summary>
	public static readonly ApiError RemoveUserToRoleFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(RemoveUserToRoleFailed)}",
			RESX.AuthenticationService_RemoveUserToRole_Failed);
}
