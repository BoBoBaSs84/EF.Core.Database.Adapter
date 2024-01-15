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
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
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
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetPagedByParameters([FromQuery] AttendanceParameters parameters, CancellationToken cancellationToken)
	{
		ErrorOr<IPagedList<AttendanceResponse>> result =
			await _attendanceService.Get(_currentUserService.UserId, parameters, false, cancellationToken);

		return Get(result, result.Value?.MetaData);
	}

	/// <summary>
	/// Returns a attendance report as a paged list for the application user filtered by the <paramref name="parameters"/>.
	/// </summary>
	/// <param name="parameters">The calendar query parameters.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the server cannot find the requested resource.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Attendance.GetPagedReportByParameters)]
	[ProducesResponseType(typeof(IPagedList<AttendanceResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetPagedReportByParameters([FromQuery] CalendarParameters parameters, CancellationToken cancellationToken)
	{
		ErrorOr<IPagedList<AttendanceResponse>> result =
			await _attendanceService.Get(_currentUserService.UserId, parameters, cancellationToken);

		return Get(result, result.Value?.MetaData);
	}

	/// <summary>
	/// Returns the attendance entry by the calendar entry identifier.
	/// </summary>
	/// <param name="calendarId">The calendar identifier.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the server cannot find the requested resource.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Attendance.GetById)]
	[ProducesResponseType(typeof(AttendanceResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetById(Guid calendarId, CancellationToken cancellationToken)
	{
		ErrorOr<AttendanceResponse> result =
			await _attendanceService.Get(_currentUserService.UserId, calendarId, false, cancellationToken);

		return Get(result);
	}

	/// <summary>
	/// Returns the attendance entry by the calendar entry date.
	/// </summary>
	/// <param name="date">The calendar entry date.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the server cannot find the requested resource.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Attendance.GetByDate)]
	[ProducesResponseType(typeof(AttendanceResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByDate(DateTime date, CancellationToken cancellationToken)
	{
		ErrorOr<AttendanceResponse> result =
			await _attendanceService.Get(_currentUserService.UserId, date, false, cancellationToken);

		return Get(result);
	}

	/// <summary>
	/// Deletes an existing attendance entry by the calendar entry identifier.
	/// </summary>
	/// <param name="calendarId">The calendar entry identifier.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the attendance was deleted.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the server cannot find the requested resource.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpDelete(Endpoints.Attendance.Delete)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete(Guid calendarId, CancellationToken cancellationToken)
	{
		ErrorOr<Deleted> result =
			await _attendanceService.Delete(_currentUserService.UserId, calendarId, cancellationToken);

		return Delete(result);
	}

	/// <summary>
	/// Deletes multiple attendance entries by the calendar entry identifiers.
	/// </summary>
	/// <param name="calendarIds">The calendar entry identifiers to delete.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the attendances were deleted.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the server cannot find the requested resource.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpDelete(Endpoints.Attendance.DeleteMultiple)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete([FromBody] IEnumerable<Guid> calendarIds, CancellationToken cancellationToken)
	{
		ErrorOr<Deleted> result =
			await _attendanceService.Delete(_currentUserService.UserId, calendarIds, cancellationToken);

		return Delete(result);
	}

	/// <summary>
	/// Creates a new attendance entry
	/// </summary>
	/// <param name="createRequest">The attendance create request.</param>
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
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Post([FromBody] AttendanceCreateRequest createRequest, CancellationToken cancellationToken)
	{
		ErrorOr<Created> result =
			await _attendanceService.Create(_currentUserService.UserId, createRequest, cancellationToken);

		return PostWithoutLocation(result);
	}

	/// <summary>
	/// Creates multiple new attendance entries.
	/// </summary>
	/// <param name="createRequest">The attendances create request.</param>
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
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> PostMultiple([FromBody] IEnumerable<AttendanceCreateRequest> createRequest, CancellationToken cancellationToken)
	{
		ErrorOr<Created> result =
			await _attendanceService.Create(_currentUserService.UserId, createRequest, cancellationToken);

		return PostWithoutLocation(result);
	}

	/// <summary>
	/// Updates a existing attendance entry.
	/// </summary>
	/// <param name="updateRequest">The attendance update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the attendance was updated.</response>
	/// <response code="400">if the provided request contains errors.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the server cannot find the requested resource.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpPut(Endpoints.Attendance.Put)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Put([FromBody] AttendanceUpdateRequest updateRequest, CancellationToken cancellationToken)
	{
		ErrorOr<Updated> result =
			await _attendanceService.Update(updateRequest, cancellationToken);

		return Put(result);
	}

	/// <summary>
	/// Updates multiple existing attendance entries.
	/// </summary>
	/// <param name="updateRequest">The attendances update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the attendances were updated.</response>
	/// <response code="400">if the provided request contains errors.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the server cannot find the requested resource.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpPut(Endpoints.Attendance.PutMultiple)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> PutMultiple([FromBody] IEnumerable<AttendanceUpdateRequest> updateRequest, CancellationToken cancellationToken)
	{
		ErrorOr<Updated> result =
			await _attendanceService.Update(updateRequest, cancellationToken);

		return Put(result);
	}
}
