using Application.Contracts.Requests;
using Application.Contracts.Responses;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Services;
using Domain.Errors;
using Domain.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

/// <summary>
/// The <see cref="AttendanceController"/> class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
[Authorize]
[Route(Endpoints.Attendance.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class AttendanceController : ApiControllerBase
{
	private readonly IAttendanceService _attendanceService;
	private readonly ICurrentUserService _currentUserService;

	/// <summary>
	/// Initializes an instance of <see cref="AttendanceController"/> class.
	/// </summary>
	/// <param name="attendanceService">The attendance service.</param>
	/// <param name="currentUserService">The current user service.</param>
	public AttendanceController(IAttendanceService attendanceService, ICurrentUserService currentUserService)
	{
		_attendanceService = attendanceService;
		_currentUserService = currentUserService;
	}

	/// <summary>
	/// Should return the attendance entities as a paged list, filtered by the parameters.
	/// </summary>
	/// <param name="parameters">The calendar day query parameters.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Attendance.GetPagedByParameters)]
	[ProducesResponseType(typeof(IPagedList<AttendanceResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetPagedByParameters([FromQuery] AttendanceParameters parameters, CancellationToken cancellationToken)
	{
		ErrorOr<IPagedList<AttendanceResponse>> result =
			await _attendanceService.GetPagedByParameters(_currentUserService.UserId, parameters, false, cancellationToken);

		return Get(result, result.Value?.MetaData);
	}

	/// <summary>
	/// Should return the attendance entity by the calendar day identifier.
	/// </summary>
	/// <param name="calendarDayId">The calendar day identifier.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Attendance.GetById)]
	[ProducesResponseType(typeof(AttendanceResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetById(int calendarDayId, CancellationToken cancellationToken)
	{
		ErrorOr<AttendanceResponse> result =
			await _attendanceService.GetById(_currentUserService.UserId, calendarDayId, false, cancellationToken);

		return Get(result);
	}

	/// <summary>
	/// Should return the attendance entity by the calendar day date.
	/// </summary>
	/// <param name="date">The calendar day date.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Attendance.GetByDate)]
	[ProducesResponseType(typeof(AttendanceResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByDate(DateTime date, CancellationToken cancellationToken)
	{
		ErrorOr<AttendanceResponse> result =
			await _attendanceService.GetByDate(_currentUserService.UserId, date, false, cancellationToken);

		return Get(result);
	}

	/// <summary>
	/// Should delete a attendance by the calendat day identifier.
	/// </summary>
	/// <param name="calendarDayId">The calendar day identifier.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the attendance was deleted.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpDelete(Endpoints.Attendance.Delete)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete(int calendarDayId, CancellationToken cancellationToken)
	{
		ErrorOr<Deleted> result =
			await _attendanceService.Delete(_currentUserService.UserId, calendarDayId, cancellationToken);

		return Delete(result);
	}

	/// <summary>
	/// Should delete multiple attendances by the calendar day identifiers.
	/// </summary>
	/// <param name="calendarDayIds">The calendar day identifiers to delete.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the attendances were deleted.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpDelete(Endpoints.Attendance.DeleteMultiple)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete([FromBody] IEnumerable<int> calendarDayIds, CancellationToken cancellationToken)
	{
		ErrorOr<Deleted> result =
			await _attendanceService.DeleteMany(_currentUserService.UserId, calendarDayIds, cancellationToken);

		return Delete(result);
	}

	/// <summary>
	/// Should create a attendance.
	/// </summary>
	/// <param name="createRequest">The attendance create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="201">If the attendance was created.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpPost(Endpoints.Attendance.Post)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Post([FromBody] AttendanceCreateRequest createRequest, CancellationToken cancellationToken)
	{
		ErrorOr<Created> result =
			await _attendanceService.Create(_currentUserService.UserId, createRequest, cancellationToken);

		return PostWithoutLocation(result);
	}

	/// <summary>
	/// Should create multiple attendances.
	/// </summary>
	/// <param name="createRequest">The attendances create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="201">If the attendances were created.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpPost(Endpoints.Attendance.PostMultiple)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> PostMultiple([FromBody] IEnumerable<AttendanceCreateRequest> createRequest, CancellationToken cancellationToken)
	{
		ErrorOr<Created> result =
			await _attendanceService.CreateMany(_currentUserService.UserId, createRequest, cancellationToken);

		return PostWithoutLocation(result);
	}

	/// <summary>
	/// Should update a attendance.
	/// </summary>
	/// <param name="updateRequest">The attendance update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the attendance was updated.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpPut(Endpoints.Attendance.Put)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Put([FromBody] AttendanceUpdateRequest updateRequest, CancellationToken cancellationToken)
	{
		ErrorOr<Updated> result =
			await _attendanceService.Update(updateRequest, cancellationToken);

		return Put(result);
	}

	/// <summary>
	/// Should update multiple attendances.
	/// </summary>
	/// <param name="updateRequest">The attendance update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the attendances were updated.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpPut(Endpoints.Attendance.PutMultiple)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> PutMultiple([FromBody] IEnumerable<AttendanceUpdateRequest> updateRequest, CancellationToken cancellationToken)
	{
		ErrorOr<Updated> result =
			await _attendanceService.UpdateMany(updateRequest, cancellationToken);

		return Put(result);
	}
}
