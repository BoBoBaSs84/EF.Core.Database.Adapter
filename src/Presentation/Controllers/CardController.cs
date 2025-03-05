using Asp.Versioning;

using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Contracts.Responses.Finance;
using BB84.Home.Application.Interfaces.Application.Services.Finance;
using BB84.Home.Application.Interfaces.Presentation.Services;
using BB84.Home.Domain.Enumerators;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;
using BB84.Home.Presentation.Attributes;
using BB84.Home.Presentation.Common;
using BB84.Home.Presentation.Controllers.Base;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BB84.Home.Presentation.Controllers;

/// <summary>
/// The bank card controller class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
/// <param name="cardService">The bank card service to use.</param>
/// <param name="currentUserService">The current user service to use.</param>
/// <param name="transactionService">The transaction service to use.</param>
[Authorize]
[Route(Endpoints.Card.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed partial class CardController(ICardService cardService, ICurrentUserService currentUserService, ITransactionService transactionService) : ApiControllerBase
{
	private readonly ICardService _cardService = cardService;
	private readonly ICurrentUserService _currentUserService = currentUserService;
	private readonly ITransactionService _transactionService = transactionService;

	/// <summary>
	/// Deletes an existing bank card by the bank card identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank card.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The resource was successfully deleted.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">Insufficient permissions to access the resource or action.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpDelete(Endpoints.Card.Delete), AuthorizeRoles(RoleType.ADMINISTRATOR)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete(Guid id, CancellationToken token)
	{
		ErrorOr<Deleted> response = await _cardService
			.Delete(id, token)
			.ConfigureAwait(false);

		return Delete(response);
	}

	/// <summary>
	/// Returns a collection of bank cards for for the application user.
	/// </summary>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Card.GetByUserId)]
	[ProducesResponseType(typeof(IEnumerable<CardResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByUserId(CancellationToken token)
	{
		ErrorOr<IEnumerable<CardResponse>> response = await _cardService
			.GetByUserId(_currentUserService.UserId, token)
			.ConfigureAwait(false);

		return Get(response);
	}

	/// <summary>
	/// Returns a bank card by the bank card identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank card.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Card.GetByCardId)]
	[ProducesResponseType(typeof(CardResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByCardId(Guid id, CancellationToken token)
	{
		ErrorOr<CardResponse> response = await _cardService
			.GetById(id, token)
			.ConfigureAwait(false);

		return Get(response);
	}

	/// <summary>
	/// Creates a bank card for the application user and bank account.
	/// </summary>
	/// <param name="id">The identifier of the bank account.</param>
	/// <param name="request">The bank card create request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="201">The resource was successfully created.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="409">The request conflicts with the target resource.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPost(Endpoints.Card.Post)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Post(Guid id, CardCreateRequest request, CancellationToken token)
	{
		ErrorOr<Created> response = await _cardService
			.Create(_currentUserService.UserId, id, request, token)
			.ConfigureAwait(false);

		return PostWithoutLocation(response);
	}

	/// <summary>
	/// Updates an existing bank card by the bank card identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank card.</param>
	/// <param name="request">The bank card update request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The resource was successfully updated.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPut(Endpoints.Card.Put)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Put(Guid id, CardUpdateRequest request, CancellationToken token)
	{
		ErrorOr<Updated> response = await _cardService
			.Update(id, request, token)
			.ConfigureAwait(false);

		return Put(response);
	}
}
