using Application.Contracts.Responses.Enumerators;
using Application.Interfaces.Application;

using Domain.Errors;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Presentation.Common;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

/// <summary>
/// The enumerator controller class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
[Route(Endpoints.Enumerator.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class EnumeratorController : ApiControllerBase
{
	private readonly ICardTypeService _cardTypeService;
	private readonly IDayTypeService _dayTypeService;

	/// <summary>
	/// Initializes an instance of the enumerator controller class.
	/// </summary>
	/// <param name="cardTypeService">The card type service to use.</param>
	/// <param name="dayTypeService">The day type service to use.</param>
	public EnumeratorController(ICardTypeService cardTypeService, IDayTypeService dayTypeService)
	{
		_cardTypeService = cardTypeService;
		_dayTypeService = dayTypeService;
	}

	/// <summary>
	/// Returns all card type enumerators.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Enumerator.CardType.GetAll)]
	[ProducesResponseType(typeof(IEnumerable<CardTypeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetAllCardTypes(CancellationToken cancellationToken)
	{
		ErrorOr<IEnumerable<CardTypeResponse>> result =
			await _cardTypeService.Get(false, cancellationToken);

		return Get(result);
	}

	/// <summary>
	/// Returns all day type enumerators.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Enumerator.DayType.GetAll)]
	[ProducesResponseType(typeof(IEnumerable<DayTypeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetAllDayTypes(CancellationToken cancellationToken)
	{
		ErrorOr<IEnumerable<DayTypeResponse>> result =
			await _dayTypeService.Get(false, cancellationToken);

		return Get(result);
	}
}
