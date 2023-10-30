using System.ComponentModel.DataAnnotations;

using Application.Contracts.Responses.Common;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;

using Asp.Versioning;

using Domain.Errors;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Presentation.Common;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

/// <summary>
/// The calendar controller class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
[Route(Endpoints.Calendar.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class CalendarController : ApiControllerBase
{
	private readonly ICalendarService _calendarDayService;

	/// <summary>
	/// Initializes an instance of the calendar controller class.
	/// </summary>
	/// <param name="calendarDayService">The calendar day service.</param>
	public CalendarController(ICalendarService calendarDayService) =>
		_calendarDayService = calendarDayService;

	/// <summary>
	/// Returns calendar entries as a paged list, filtered by the parameters.
	/// </summary>
	/// <param name="parameters">The calendar query parameters.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Calendar.GetPagedByParameters)]
	[ProducesResponseType(typeof(IPagedList<CalendarResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetPagedByParameters([FromQuery] CalendarParameters parameters, CancellationToken cancellationToken)
	{
		ErrorOr<IPagedList<CalendarResponse>> result =
			await _calendarDayService.Get(parameters, false, cancellationToken);

		return Get(result, result.Value?.MetaData);
	}

	/// <summary>
	/// Returns the calendar entry by its date.
	/// </summary>
	/// <param name="date">The date of the calendar entry.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Calendar.GetByDate)]
	[ProducesResponseType(typeof(CalendarResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByDate([DataType(DataType.Date)] DateTime date, CancellationToken cancellationToken)
	{
		ErrorOr<CalendarResponse> result =
			await _calendarDayService.Get(date, false, cancellationToken);

		return Get(result);
	}

	/// <summary>
	/// Returns the calendar entry by its identifier.
	/// </summary>
	/// <param name="id">The identifier of the calendar entry.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Calendar.GetById)]
	[ProducesResponseType(typeof(CalendarResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
	{
		ErrorOr<CalendarResponse> result =
			await _calendarDayService.Get(id, false, cancellationToken);

		return Get(result);
	}

	/// <summary>
	/// Returns the current calendar entry.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Calendar.GetCurrent)]
	[ProducesResponseType(typeof(CalendarResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetCurrent(CancellationToken cancellationToken)
	{
		ErrorOr<CalendarResponse> result =
			await _calendarDayService.Get(false, cancellationToken);

		return Get(result);
	}
}
