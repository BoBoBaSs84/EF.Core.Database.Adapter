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
	private readonly IEnumeratorService _enumeratorService;

	/// <summary>
	/// Initializes an instance of the enumerator controller class.
	/// </summary>
	/// <param name="enumeratorService">The enumerator service to use.</param>
	public EnumeratorController(IEnumeratorService enumeratorService)
		=> _enumeratorService = enumeratorService;

	/// <summary>
	/// Returns all card type enumerators.
	/// </summary>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Enumerator.CardType.GetAll)]
	[ProducesResponseType(typeof(IEnumerable<CardTypeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public IActionResult GetCardTypes()
	{
		ErrorOr<IEnumerable<CardTypeResponse>> result = _enumeratorService.GetCardTypes();
		return Get(result);
	}

	/// <summary>
	/// Returns all day type enumerators.
	/// </summary>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Enumerator.DayType.GetAll)]
	[ProducesResponseType(typeof(IEnumerable<DayTypeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public IActionResult GetDayTypes()
	{
		ErrorOr<IEnumerable<DayTypeResponse>> result = _enumeratorService.GetDayTypes();
		return Get(result);
	}
}
