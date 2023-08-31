using System.ComponentModel.DataAnnotations;

using Application.Contracts.Responses;
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
/// The <see cref="CalendarDayController"/> class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
[Route(Endpoints.CalendarDay.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class CalendarDayController : ApiControllerBase
{
	private readonly ICalendarDayService _calendarDayService;

	/// <summary>
	/// Initializes an instance of <see cref="CalendarDayController"/> class.
	/// </summary>
	/// <param name="calendarDayService">The calendar day service.</param>
	public CalendarDayController(ICalendarDayService calendarDayService) =>
		_calendarDayService = calendarDayService;

	/// <summary>
	/// Should return the calendar day entities as a paged list, filtered by the parameters.
	/// </summary>
	/// <param name="parameters">The calendar day query parameters.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.CalendarDay.GetPagedByParameters)]
	[ProducesResponseType(typeof(IPagedList<CalendarDayResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetPagedByParameters([FromQuery] CalendarDayParameters parameters, CancellationToken cancellationToken)
	{
		ErrorOr<IPagedList<CalendarDayResponse>> result =
			await _calendarDayService.Get(parameters, false, cancellationToken);

		return Get(result, result.Value?.MetaData);
	}

	/// <summary>
	/// Should return the calendar day by its date.
	/// </summary>
	/// <param name="date">The date of the calendar day.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.CalendarDay.GetByDate)]
	[ProducesResponseType(typeof(CalendarDayResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByDate([DataType(DataType.Date)] DateTime date, CancellationToken cancellationToken)
	{
		ErrorOr<CalendarDayResponse> result =
			await _calendarDayService.Get(date, false, cancellationToken);

		return Get(result);
	}

	/// <summary>
	/// Should return the calendar day by its identifier.
	/// </summary>
	/// <param name="id">The identifier of the calendar day.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.CalendarDay.GetById)]
	[ProducesResponseType(typeof(CalendarDayResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
	{
		ErrorOr<CalendarDayResponse> result =
			await _calendarDayService.Get(id, false, cancellationToken);

		return Get(result);
	}

	/// <summary>
	/// Should return the current calendar day.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.CalendarDay.GetCurrent)]
	[ProducesResponseType(typeof(CalendarDayResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetCurrent(CancellationToken cancellationToken)
	{
		ErrorOr<CalendarDayResponse> result =
			await _calendarDayService.Get(false, cancellationToken);

		return Get(result);
	}
}
