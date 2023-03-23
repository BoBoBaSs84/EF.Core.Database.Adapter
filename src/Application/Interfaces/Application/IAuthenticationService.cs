using Application.Contracts.Requests.Identity;
using Application.Contracts.Responses.Identity;
using Domain.Errors;
using Domain.Results;

namespace Application.Interfaces.Application;

/// <summary>
/// The authentication service interface.
/// </summary>
public interface IAuthenticationService
{
	/// <summary>
	/// Creates a new application user.
	/// </summary>
	/// <param name="createRequest">The user create reqeust.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> CreateUser(UserCreateRequest createRequest);

	/// <summary>
	/// Updates an existing application user.
	/// </summary>
	/// <param name="userId">The user identifier of the user to update.</param>
	/// <param name="updateRequest">The user update reqeust.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> UpdateUser(int userId, UserUpdateRequest updateRequest);

	/// <summary>
	/// Authenticates an existing application user.
	/// </summary>
	/// <param name="authRequest">The authentication reqeust.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<AuthenticationResponse>> Authenticate(AuthenticationRequest authRequest);
}
