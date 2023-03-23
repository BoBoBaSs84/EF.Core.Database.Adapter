using Application.Errors.Base;
using Domain.Extensions;
using System.Globalization;
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
	private static readonly CultureInfo CurrentCulture = Domain.Statics.CurrentCulture;
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
	/// <param name="userName">The user name.</param>
	/// <returns><see cref="ApiError"/></returns>
	public static ApiError UserNotFound(string userName) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(UserNotFound)}",
			RESX.AuthenticationService_User_NotFound.Format(CurrentCulture, userName));

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
}
