using Application.Contracts.Requests.Identity;
using Application.Contracts.Responses.Identity;

using Domain.Errors;
using Domain.Results;

namespace Application.Interfaces.Application.Services.Identity;

/// <summary>
/// The authentication service interface.
/// </summary>
public interface IAuthenticationService
{
	/// <summary>
	/// Adds the user with the <paramref name="userId"/> to the role
	/// with the <paramref name="roleId"/>.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="roleId">The identifier of the role.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> AddUserToRole(Guid userId, Guid roleId);

	/// <summary>
	/// Authenticates an existing application user.
	/// </summary>
	/// <param name="request">The authentication reqeust.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<AuthenticationResponse>> Authenticate(AuthenticationRequest request);

	/// <summary>
	/// Should retrieve all apllication users.
	/// </summary>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IEnumerable<UserResponse>>> GetAllUser();

	/// <summary>
	/// Creates a new application user.
	/// </summary>
	/// <param name="request">The user create reqeust.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> CreateUser(UserCreateRequest request);

	/// <summary>
	/// Should return the application user by the identifier.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<UserResponse>> GetUserById(Guid userId);

	/// <summary>
	/// Should return the application user by the user name.
	/// </summary>
	/// <param name="userName">the user name.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<UserResponse>> GetUserByName(string userName);

	/// <summary>
	/// Refreshes the user's access and refresh token.
	/// </summary>
	/// <param name="request">The token request to use.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<AuthenticationResponse>> RefreshAccessToken(TokenRequest request);

	/// <summary>
	/// Revokes the current user's refresh token.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> RevokeRefreshToken(Guid userId);

	/// <summary>
	/// Removes the user with the <paramref name="userId"/> to the role
	/// with the <paramref name="roleId"/>.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="roleId">The identifier of the role.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> RemoveUserFromRole(Guid userId, Guid roleId);

	/// <summary>
	/// Updates an existing application user.
	/// </summary>
	/// <param name="userId">The user identifier of the user to update.</param>
	/// <param name="request">The user update reqeust.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> UpdateUser(Guid userId, UserUpdateRequest request);
}
