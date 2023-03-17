using Application.Contracts.Requests.Identity;
using Application.Interfaces.Application;
using Domain.Errors;
using Domain.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

/// <summary>
/// The <see cref="AuthenticationController"/> class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
[Route(Endpoints.Authentication.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class AuthenticationController : ApiControllerBase
{
	private readonly IAuthenticationService _authenticationService;

	/// <summary>
	/// Initializes an instance of <see cref="AuthenticationController"/> class.
	/// </summary>
	/// <param name="authenticationService">The authentication service.</param>
	public AuthenticationController(IAuthenticationService authenticationService) =>
		_authenticationService = authenticationService;

	/// <summary>
	/// Creates a new user.
	/// </summary>
	/// <param name="createRequest">The user create request.</param>
	/// <response code="201">If the new user was created.</response>
	/// <response code="400">If something is not right with the request.</response>
	[HttpPost(Endpoints.Authentication.CreateUser)]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> CreateUser([FromBody] UserCreateRequest createRequest)
	{
		ErrorOr<Created> result = await _authenticationService.CreateUser(createRequest);
		return Get(result);
	}

	/// <summary>
	/// Updates an existing user.
	/// </summary>
	/// <param name="userName">The user to update.</param>
	/// <param name="updateRequest">The user update request.</param>
	/// <response code="200">If the user was updated.</response>
	/// <response code="400">If something is not right with the request.</response>
	/// <response code="401">If you are not authorize to update the user.</response>
	/// <response code="404">If the user to update was not found.</response>
	[HttpPut(Endpoints.Authentication.UpdateUser)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> UpdateUser(string userName, [FromBody] UserUpdateRequest updateRequest)
	{
		ErrorOr<Updated> result = await _authenticationService.UpdateUser(userName, updateRequest);
		return Get(result);
	}

	/// <summary>
	/// Checks if the user can login.
	/// </summary>
	/// <param name="loginRequest">The user login request.</param>
	/// <response code="200">If the user can login.</response>
	/// <response code="400">If something is not right with the request.</response>
	/// <response code="401">If you are not authorize to login.</response>
	/// <response code="404">If the user to login was not found.</response>
	[HttpPost(Endpoints.Authentication.AuthenticateUser)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> AuthenticateUser([FromBody] UserLoginRequest loginRequest)
	{
		ErrorOr<Success> result = await _authenticationService.AuthenticateUser(loginRequest);
		return Get(result);
	}
}
