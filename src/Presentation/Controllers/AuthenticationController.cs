using Application.Contracts.Requests.Identity;
using Application.Contracts.Responses.Identity;
using Application.Interfaces.Application;
using Domain.Errors;
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
	/// Should authenticate an existing application user.
	/// </summary>
	/// <param name="loginRequest">The user login request.</param>
	/// <response code="200">If the user can login.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="500">If the something went wrong.</response>
	[HttpPost(Endpoints.Authentication.Authenticate)]
	[ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest loginRequest)
	{
		ErrorOr<AuthenticationResponse> result =
			await _authenticationService.Authenticate(loginRequest);

		return Get(result);
	}
}
