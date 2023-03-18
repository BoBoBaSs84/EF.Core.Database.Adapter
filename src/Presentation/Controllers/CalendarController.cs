using Application.Contracts.Features.Requests;
using Application.Contracts.Features.Responses;
using Application.Contracts.Responses;
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
/// The <see cref="CalendarController"/> class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
[Route(Endpoints.Calendar.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class CalendarController : ApiControllerBase
{
	private readonly ICalendarService _calendarService;

	/// <summary>
	/// Initializes an instance of <see cref="CalendarController"/> class.
	/// </summary>
	/// <param name="calendarService">The calendar service.</param>
	public CalendarController(ICalendarService calendarService) =>
		_calendarService = calendarService;

	/// <summary>
	/// Should return all calendar entries as a paged list.
	/// </summary>
	/// <param name="parameters">The calendar query parameters.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="500">If the something went wrong.</response>
	[HttpGet(Endpoints.Calendar.GetAll)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetAll([FromQuery] CalendarParameters parameters)
	{
		ErrorOr<IPagedList<CalendarResponse>> result = await _calendarService.GetAllPaged(parameters);

		string jsonMetadata = result.Value.MetaData.ToJsonString();

		Response.Headers.Add(HHC.Pagination, jsonMetadata);

		return Get(result);
	}
}
