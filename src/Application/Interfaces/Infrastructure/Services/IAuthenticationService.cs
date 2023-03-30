using Application.Contracts.Requests.Identity;
using Application.Contracts.Responses.Identity;
using Domain.Errors;
using Domain.Results;

namespace Application.Interfaces.Infrastructure.Services;

/// <summary>
/// The authentication service interface.
/// </summary>
public interface IAuthenticationService
{
	/// <summary>
	/// Should add a user to a certain role.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="roleName">The role the user should be added to.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> AddUserToRole(int userId, string roleName);

	/// <summary>
	/// Authenticates an existing application user.
	/// </summary>
	/// <param name="authRequest">The authentication reqeust.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<AuthenticationResponse>> Authenticate(AuthenticationRequest authRequest);

	/// <summary>
	/// Should retrieve all apllication users.
	/// </summary>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IEnumerable<UserResponse>>> GetAll();

	/// <summary>
	/// Creates a new application user.
	/// </summary>
	/// <param name="createRequest">The user create reqeust.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> CreateUser(UserCreateRequest createRequest);

	/// <summary>
	/// Should return the application user by the identifier.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<UserResponse>> GetUserById(int userId);

	/// <summary>
	/// Should return the application user by the user name.
	/// </summary>
	/// <param name="userName">the user name.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<UserResponse>> GetUserByName(string userName);

	/// <summary>
	/// Should remove a user from a certain role.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="roleName">The role the user should be removed from.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> RemoveUserToRole(int userId, string roleName);

	/// <summary>
	/// Updates an existing application user.
	/// </summary>
	/// <param name="userId">The user identifier of the user to update.</param>
	/// <param name="updateRequest">The user update reqeust.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> UpdateUser(int userId, UserUpdateRequest updateRequest);
}
