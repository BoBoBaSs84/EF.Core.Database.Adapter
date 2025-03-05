using Asp.Versioning;

using BB84.Home.Application.Contracts.Requests.Identity;
using BB84.Home.Application.Contracts.Responses.Identity;
using BB84.Home.Application.Interfaces.Application.Services.Identity;
using BB84.Home.Application.Interfaces.Presentation.Services;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Presentation.Common;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

/// <summary>
/// The authentication controller class.
/// </summary>
/// <param name="authenticationService">The authentication service instance to use.</param>
/// <param name="currentUserService">The current user service instance to use.</param>
[ApiVersion(Versioning.CurrentVersion)]
[Route(Endpoints.Authentication.BaseUri)]
public sealed class AuthenticationController(IAuthenticationService authenticationService, ICurrentUserService currentUserService) : ApiControllerBase
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

	/// <summary>
	/// Refreshes the user's access and refresh token.
	/// </summary>
	/// <param name="request">The token request to use.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="500">Something internal went terribly wrong.</response>	
	[HttpPost(Endpoints.Authentication.Token.Refresh)]
	[ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> RefreshAccessToken([FromBody] TokenRequest request)
	{
		ErrorOr<AuthenticationResponse> result = await authenticationService
			.RefreshAccessToken(request)
			.ConfigureAwait(false);

		return Get(result);
	}

	/// <summary>
	/// Revokes the current user's refresh token.
	/// </summary>
	/// <response code="200">The resource was successfully deleted.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="500">Something internal went terribly wrong.</response>	
	[Authorize, HttpDelete(Endpoints.Authentication.Token.Revoke)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> RevokeRefreshToken()
	{
		ErrorOr<Deleted> result = await authenticationService
			.RevokeRefreshToken(currentUserService.UserId)
			.ConfigureAwait(false);

		return Delete(result);
	}
}
