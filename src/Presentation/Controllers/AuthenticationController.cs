using Application.Contracts.Requests.Identity;
using Application.Contracts.Responses.Identity;
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
	/// Should create a new application user.
	/// </summary>
	/// <param name="createRequest">The user create request.</param>
	/// <response code="201">If the new user was created.</response>
	/// <response code="400">If something is wrong with the request.</response>
	/// <response code="500">If the something went wrong.</response>
	[HttpPost(Endpoints.Authentication.CreateUser)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> CreateUser([FromBody] UserCreateRequest createRequest)
	{
		ErrorOr<Created> result =
			await _authenticationService.CreateUser(createRequest);
		
		return PostWithoutLocation(result);
	}

	/// <summary>
	/// Should update an existing application user.
	/// </summary>
	/// <param name="userName">The user to update.</param>
	/// <param name="updateRequest">The user update request.</param>
	/// <response code="200">If the user was updated.</response>
	/// <response code="400">If something is wrong with the request.</response>
	/// <response code="401">If you are not authorize to update the user.</response>
	/// <response code="404">If the user to update was not found.</response>
	/// <response code="500">If the something went wrong.</response>
	[HttpPut(Endpoints.Authentication.UpdateUser)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateUser(string userName, [FromBody] UserUpdateRequest updateRequest)
	{
		ErrorOr<Updated> result =
			await _authenticationService.UpdateUser(userName, updateRequest);
		
		return Get(result);
	}

	/// <summary>
	/// Should authenticate an existing application user.
	/// </summary>
	/// <param name="loginRequest">The user login request.</param>
	/// <response code="200">If the user can login.</response>
	/// <response code="400">If something is wrong with the request.</response>
	/// <response code="401">If you are not authorize to login.</response>
	/// <response code="404">If the user to login was not found.</response>
	/// <response code="500">If the something went wrong.</response>
	[HttpPost(Endpoints.Authentication.AuthenticateUser)]
	[ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticationRequest loginRequest)
	{
		ErrorOr<AuthenticationResponse> result =
			await _authenticationService.Authenticate(loginRequest);
		
		return Get(result);
	}
}
