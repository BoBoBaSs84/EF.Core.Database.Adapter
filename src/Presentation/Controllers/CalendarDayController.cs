using Application.Contracts.Responses;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;
using Domain.Errors;
using Domain.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common;
using Presentation.Controllers.Base;
using HHC = Presentation.Constants.WebConstants.HttpHeaders;

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
	/// <response code="200">If the result is returned.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.CalendarDay.GetPagedByParameters)]
	[ProducesResponseType(typeof(IPagedList<CalendarDayResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetPagedByParameters([FromQuery] CalendarDayParameters parameters)
	{
		ErrorOr<IPagedList<CalendarDayResponse>> result = await _calendarDayService.GetPagedByParameters(parameters);

		if (!result.IsError)
		{
			string jsonMetadata = result.Value.MetaData.ToJsonString();
			Response.Headers.Add(HHC.Pagination, jsonMetadata);
		}

		return Get(result);
	}
}
