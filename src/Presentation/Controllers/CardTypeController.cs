using Application.Contracts.Responses.Enumerator;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;
using Domain.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

/// <summary>
/// The <see cref="CardTypeController"/> class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
[Route(Endpoints.CardType.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class CardTypeController : ApiControllerBase
{
	private readonly ICardTypeService _cardTypeService;

	/// <summary>
	/// Initializes an instance of the <see cref="DayTypeController"/> class.
	/// </summary>
	/// <param name="cardTypeService">The card type service.</param>
	public CardTypeController(ICardTypeService cardTypeService) =>
		_cardTypeService = cardTypeService;

	/// <summary>
	/// Should return all card type entities.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.CardType.GetAll)]
	[ProducesResponseType(typeof(IEnumerable<CardTypeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
	{
		ErrorOr<IEnumerable<CardTypeResponse>> result =
			await _cardTypeService.GetAll(false, cancellationToken);
		
		return Get(result);
	}

	/// <summary>
	/// Should return the card type by its identifier.
	/// </summary>
	/// <param name="id">The identifier of the card type.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.DayType.GetById)]
	[ProducesResponseType(typeof(IPagedList<CardTypeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
	{
		ErrorOr<CardTypeResponse> result =
			await _cardTypeService.GetById(id, false, cancellationToken);
		
		return Get(result);
	}

	/// <summary>
	/// Should return the card type by its name.
	/// </summary>
	/// <param name="name">The name of the card type.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.DayType.GetByName)]
	[ProducesResponseType(typeof(IPagedList<CardTypeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByName(string name, CancellationToken cancellationToken)
	{
		ErrorOr<CardTypeResponse> result =
			await _cardTypeService.GetByName(name, false, cancellationToken);
		
		return Get(result);
	}
}
