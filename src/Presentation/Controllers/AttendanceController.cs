using Application.Contracts.Requests.Attendance;
using Application.Contracts.Responses.Attendance;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;
using Application.Interfaces.Presentation.Services;

using Asp.Versioning;

using Domain.Errors;
using Domain.Results;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Presentation.Common;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

/// <summary>
/// The attendance controller class.
/// </summary>
/// <param name="attendanceService">The attendance service to use.</param>
/// <param name="currentUserService">The current user service to use.</param>
[Authorize]
[Route(Endpoints.Attendance.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class AttendanceController(IAttendanceService attendanceService, ICurrentUserService currentUserService) : ApiControllerBase
{
	private readonly IAttendanceService _attendanceService = attendanceService;
	private readonly ICurrentUserService _currentUserService = currentUserService;

	/// <summary>
	/// Returns multiple attendances as a paged list for the application user filtered by the <paramref name="parameters"/>.
	/// </summary>
	/// <param name="parameters">The attendance query parameters.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the server cannot find the requested resource.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Attendance.GetPagedByParameters)]
	[ProducesResponseType(typeof(IPagedList<AttendanceResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetPagedByParameters([FromQuery] AttendanceParameters parameters, CancellationToken cancellationToken)
	{
		ErrorOr<IPagedList<AttendanceResponse>> result = await _attendanceService
			.GetPagedListByParameters(_currentUserService.UserId, parameters, cancellationToken)
			.ConfigureAwait(false);

		return Get(result, result.Value?.MetaData);
	}

	/// <summary>
	/// Returns the attendance entry by the calendar entry date.
	/// </summary>
	/// <param name="date">The attendance date to use.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the server cannot find the requested resource.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Attendance.GetByDate)]
	[ProducesResponseType(typeof(AttendanceResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByDate(DateTime date, CancellationToken cancellationToken)
	{
		ErrorOr<AttendanceResponse> result = await _attendanceService
			.GetByDate(_currentUserService.UserId, date, cancellationToken)
			.ConfigureAwait(false);

		return Get(result);
	}

	/// <summary>
	/// Deletes an existing attendance entry by the calendar entry date.
	/// </summary>
	/// <param name="date">The attendance date to delete.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the attendance was deleted.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the server cannot find the requested resource.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpDelete(Endpoints.Attendance.Delete)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete(DateTime date, CancellationToken cancellationToken)
	{
		ErrorOr<Deleted> result = await _attendanceService
			.Delete(_currentUserService.UserId, date, cancellationToken)
			.ConfigureAwait(false);

		return Delete(result);
	}

	/// <summary>
	/// Deletes multiple attendance entries by the calendar entry identifiers.
	/// </summary>
	/// <param name="dates">The attendance dates to delete.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the attendances were deleted.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the server cannot find the requested resource.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpDelete(Endpoints.Attendance.DeleteMultiple)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete([FromBody] IEnumerable<DateTime> dates, CancellationToken cancellationToken)
	{
		ErrorOr<Deleted> result = await _attendanceService
			.Delete(_currentUserService.UserId, dates, cancellationToken)
			.ConfigureAwait(false);

		return Delete(result);
	}

	/// <summary>
	/// Creates a new attendance entry
	/// </summary>
	/// <param name="request">The attendance create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="201">If the attendance was created.</response>
	/// <response code="400">if the provided request contains errors.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the server cannot find the requested resource.</response>
	/// <response code="409">Conflict with the current state of the target resource.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpPost(Endpoints.Attendance.Post)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Post([FromBody] AttendanceCreateRequest request, CancellationToken cancellationToken)
	{
		ErrorOr<Created> result = await _attendanceService
			.Create(_currentUserService.UserId, request, cancellationToken)
			.ConfigureAwait(false);

		return PostWithoutLocation(result);
	}

	/// <summary>
	/// Creates multiple new attendance entries.
	/// </summary>
	/// <param name="requests">The attendances create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="201">If the attendances were created.</response>
	/// <response code="400">if the provided request contains errors.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the server cannot find the requested resource.</response>
	/// <response code="409">Conflict with the current state of the target resource.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpPost(Endpoints.Attendance.PostMultiple)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> PostMultiple([FromBody] IEnumerable<AttendanceCreateRequest> requests, CancellationToken cancellationToken)
	{
		ErrorOr<Created> result = await _attendanceService
			.Create(_currentUserService.UserId, requests, cancellationToken)
			.ConfigureAwait(false);

		return PostWithoutLocation(result);
	}

	/// <summary>
	/// Updates a existing attendance entry.
	/// </summary>
	/// <param name="request">The attendance update request to use.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the attendance was updated.</response>
	/// <response code="400">if the provided request contains errors.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the server cannot find the requested resource.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpPut(Endpoints.Attendance.Put)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Put([FromBody] AttendanceUpdateRequest request, CancellationToken cancellationToken)
	{
		ErrorOr<Updated> result = await _attendanceService
			.Update(request, cancellationToken)
			.ConfigureAwait(false);

		return Put(result);
	}

	/// <summary>
	/// Updates multiple existing attendance entries.
	/// </summary>
	/// <param name="requests">The attendance update requests to use.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the attendances were updated.</response>
	/// <response code="400">if the provided request contains errors.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the server cannot find the requested resource.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpPut(Endpoints.Attendance.PutMultiple)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> PutMultiple([FromBody] IEnumerable<AttendanceUpdateRequest> requests, CancellationToken cancellationToken)
	{
		ErrorOr<Updated> result = await _attendanceService
			.Update(requests, cancellationToken)
			.ConfigureAwait(false);

		return Put(result);
	}
}
