using Application.Contracts.Requests.Identity;
using Application.Contracts.Responses.Identity;
using Application.Interfaces.Application.Identity;

using Asp.Versioning;

using Domain.Errors;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Presentation.Common;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

/// <summary>
/// The authentication controller class.
/// </summary>
/// <param name="authenticationService">The authentication service instance to use.</param>
[ApiVersion(Versioning.CurrentVersion)]
[Route(Endpoints.Authentication.BaseUri)]
public sealed class AuthenticationController(IAuthenticationService authenticationService) : ApiControllerBase
{
	/// <summary>
	/// Authenticates an existing application user.
	/// </summary>
	/// <param name="request">The authentication request to use.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPost(Endpoints.Authentication.Authenticate)]
	[ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request)
	{
		ErrorOr<AuthenticationResponse> result = await authenticationService
			.Authenticate(request)
			.ConfigureAwait(false);

		return Get(result);
	}
}
