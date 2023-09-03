using System.ComponentModel.DataAnnotations;

using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Interfaces.Application;
using Application.Interfaces.Presentation.Services;

using Domain.Errors;
using Domain.Results;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Presentation.Attributes;
using Presentation.Common;
using Presentation.Controllers.Base;

using RegexPatterns = Domain.Constants.DomainConstants.RegexPatterns;
using Roles = Domain.Enumerators.RoleType;

namespace Presentation.Controllers;

/// <summary>
/// The bank card controller class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
[Authorize]
[Route(Endpoints.Card.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class CardController : ApiControllerBase
{
	private readonly ICardService _cardService;
	private readonly ICurrentUserService _currentUserService;

	/// <summary>
	/// Initializes an instance of the bank card controller class.
	/// </summary>
	/// <param name="cardService">The bank card service to use.</param>
	/// <param name="currentUserService">The current user service to use.</param>
	public CardController(ICardService cardService, ICurrentUserService currentUserService)
	{
		_cardService = cardService;
		_currentUserService = currentUserService;
	}

	/// <summary>
	/// Deletes an existing bank card by the given <paramref name="cardId"/>.
	/// </summary>
	/// <param name="cardId">The bank card identifier.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The deleted response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the bank card to delete was not found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpDelete(Endpoints.Card.Delete), AuthorizeRoles(Roles.ADMINISTRATOR)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete(Guid cardId, CancellationToken cancellationToken)
	{
		ErrorOr<Deleted> result =
			await _cardService.Delete(_currentUserService.UserId, cardId, cancellationToken);

		return Delete(result);
	}

	/// <summary>
	/// Gets a collection of bank cards.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If no bank card records were found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpGet(Endpoints.Card.GetAll)]
	[ProducesResponseType(typeof(IEnumerable<CardResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
	{
		ErrorOr<IEnumerable<CardResponse>> result =
			await _cardService.Get(_currentUserService.UserId, false, cancellationToken);

		return Get(result);
	}

	/// <summary>
	/// Gets an existing bank card by the given <paramref name="cardId"/>.
	/// </summary>
	/// <param name="cardId">The bank card identifier.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If no bank card record was found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpGet(Endpoints.Card.GetById)]
	[ProducesResponseType(typeof(CardResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetById(Guid cardId, CancellationToken cancellationToken)
	{
		ErrorOr<CardResponse> result =
			await _cardService.Get(_currentUserService.UserId, cardId, false, cancellationToken);

		return Get(result);
	}

	/// <summary>
	/// Gets an existing bank card by the given <paramref name="pam"/>.
	/// </summary>
	/// <param name="pam">The payment card number.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If no bank card record was found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpGet(Endpoints.Card.GetByNumber)]
	[ProducesResponseType(typeof(CardResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByNumber([RegularExpression(RegexPatterns.CC)] string pam, CancellationToken cancellationToken)
	{
		ErrorOr<CardResponse> result =
			await _cardService.Get(_currentUserService.UserId, pam, false, cancellationToken);

		return Get(result);
	}

	/// <summary>
	/// Creates a bank card.
	/// </summary>
	/// <param name="accountId">The bank account identifier.</param>
	/// <param name="request">The bank card create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="201">The created response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="409">Conflicts occured during creation of the resource.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpPost(Endpoints.Card.Post)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Post(Guid accountId, CardCreateRequest request, CancellationToken cancellationToken)
	{
		ErrorOr<Created> result =
			await _cardService.Create(_currentUserService.UserId, accountId, request, cancellationToken);

		return PostWithoutLocation(result);
	}

	/// <summary>
	/// Updates an existing bank card.
	/// </summary>
	/// <param name="request">The bank card update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The updated response.</response>
	/// <response code="400">The update request is incorrect.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpPut(Endpoints.Card.Put)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Put(CardUpdateRequest request, CancellationToken cancellationToken)
	{
		ErrorOr<Updated> result =
			await _cardService.Update(_currentUserService.UserId, request, cancellationToken);

		return Put(result);
	}
}
