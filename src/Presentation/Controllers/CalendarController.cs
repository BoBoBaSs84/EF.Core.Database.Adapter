﻿using System.ComponentModel.DataAnnotations;

using Asp.Versioning;

using BB84.Home.Application.Contracts.Responses.Common;
using BB84.Home.Application.Features.Requests;
using BB84.Home.Application.Features.Responses;
using BB84.Home.Application.Interfaces.Application.Services.Common;
using BB84.Home.Domain.Errors;
using BB84.Home.Presentation.Common;
using BB84.Home.Presentation.Controllers.Base;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BB84.Home.Presentation.Controllers;

/// <summary>
/// The calendar controller class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
/// <param name="calendarService">The calendar service instance to use.</param>
[Route(Endpoints.Calendar.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class CalendarController(ICalendarService calendarService) : ApiControllerBase
{
	private readonly ICalendarService _calendarService = calendarService;

	/// <summary>
	/// Returns calendar entries as a paged list, filtered by the parameters.
	/// </summary>
	/// <param name="parameters">The calendar query parameters to use.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Calendar.GetPagedByParameters)]
	[ProducesResponseType(typeof(IPagedList<CalendarResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public IActionResult GetPagedByParameters([FromQuery] CalendarParameters parameters)
	{
		ErrorOr<IPagedList<CalendarResponse>> result = _calendarService.GetPagedByParameters(parameters);
		return Get(result, result.Value.MetaData);
	}

	/// <summary>
	/// Returns the calendar entry by its date.
	/// </summary>
	/// <param name="date">The date of the calendar entry.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Calendar.GetByDate)]
	[ProducesResponseType(typeof(CalendarResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public IActionResult GetByDate([DataType(DataType.Date)] DateTime date)
	{
		ErrorOr<CalendarResponse> result = _calendarService.GetByDate(date);
		return Get(result);
	}

	/// <summary>
	/// Returns the current calendar entry.
	/// </summary>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Calendar.GetCurrent)]
	[ProducesResponseType(typeof(CalendarResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public IActionResult GetCurrent()
	{
		ErrorOr<CalendarResponse> result = _calendarService.GetCurrent();
		return Get(result);
	}
}
