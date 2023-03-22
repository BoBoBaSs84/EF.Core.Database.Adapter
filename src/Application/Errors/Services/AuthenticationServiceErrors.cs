using Application.Errors.Base;
using Domain.Extensions;
using RESX = Application.Properties.Resources;

namespace Application.Errors.Services;

/// <summary>
/// The static <see cref="AuthenticationServiceErrors"/> class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the authentication service.
/// </remarks>
public static class AuthenticationServiceErrors
{
	private const string ErrorPrefix = $"{nameof(AuthenticationServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the user authentication.
	/// </summary>
	public static readonly ApiError AuthenticateUserFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(AuthenticateUserFailed)}",
			RESX.AuthenticationServiceErrors_AuthenticateFailed);

	/// <summary>
	/// Error that indicates an exception during the user authentication.
	/// </summary>
	/// <param name="userName">The user name.</param>
	/// <returns><see cref="ApiError"/></returns>
	public static ApiError UserNotFound(string userName) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(UserNotFound)}",
			RESX.AuthenticationServiceErrors_UserNotFound.FormatInvariant(userName));

	/// <summary>
	/// Error that indicates an exception during the user authentication.
	/// </summary>
	/// <param name="userName">The user name.</param>
	/// <returns><see cref="ApiError"/></returns>
	public static ApiError UserUnauthorized(string userName) =>
		ApiError.CreateUnauthorized($"{ErrorPrefix}.{nameof(UserUnauthorized)}",
			RESX.AuthenticationServiceErrors_UserUnauthorized.FormatInvariant(userName));

	/// <summary>
	/// Error that indicates an exception during the user creation.
	/// </summary>
	public static readonly ApiError CreateUserFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateUserFailed)}",
			RESX.AuthenticationServiceErrors_CreateUserFailed);

	/// <summary>
	/// Error that indicates an exception during the user creation.
	/// </summary>
	public static readonly ApiError CreateUserRolesFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateUserRolesFailed)}",
			RESX.AuthenticationServiceErrors_CreateUserRolesFailed);

	/// <summary>
	/// Error that indicates an exception during the user update.
	/// </summary>
	public static readonly ApiError UpdateUserFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(UpdateUserFailed)}",
			RESX.AuthenticationServiceErrors_UpdateUserFailed);

	/// <summary>
	/// Error that indicates an exception during the identity opertations.
	/// </summary>
	/// <param name="code">The identity error code.</param>
	/// <param name="description">The identity error description.</param>
	public static ApiError IdentityError(string code, string description) =>
		ApiError.CreateBadRequest($"{ErrorPrefix}.{nameof(IdentityError)}",
			$"{code} - {description}");
}
