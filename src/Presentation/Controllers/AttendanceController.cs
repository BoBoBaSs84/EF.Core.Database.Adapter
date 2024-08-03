using Application.Contracts.Requests.Attendance;
using Application.Contracts.Responses.Attendance;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application.Attendance;
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
	/// <summary>
	/// Deletes an attendance entry by the provided <paramref name="id"/>.
	/// </summary>
	/// <param name="id">The attendance entry identifier to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The resource was successfully deleted.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpDelete(Endpoints.Attendance.DeleteById)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete(Guid id, CancellationToken token)
	{
		ErrorOr<Deleted> result = await attendanceService
			.DeleteById(id, token)
			.ConfigureAwait(false);

		return Delete(result);
	}

	/// <summary>
	/// Deletes multiple attendance entries by the provided <paramref name="ids"/>.
	/// </summary>
	/// <param name="ids">The attendance entry identifiers to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The resource was successfully deleted.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpDelete(Endpoints.Attendance.DeleteByIds)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete([FromBody] IEnumerable<Guid> ids, CancellationToken token)
	{
		ErrorOr<Deleted> result = await attendanceService
			.DeleteByIds(ids, token)
			.ConfigureAwait(false);

		return Delete(result);
	}

	/// <summary>
	/// Returns multiple attendances as a paged list for the application user filtered by the <paramref name="parameters"/>.
	/// </summary>
	/// <param name="parameters">The attendance query parameters.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Attendance.GetPagedByParameters)]
	[ProducesResponseType(typeof(IPagedList<AttendanceResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetPagedByParameters([FromQuery] AttendanceParameters parameters, CancellationToken token)
	{
		ErrorOr<IPagedList<AttendanceResponse>> result = await attendanceService
			.GetPagedByParameters(currentUserService.UserId, parameters, token)
			.ConfigureAwait(false);

		return Get(result, result.Value.MetaData);
	}

	/// <summary>
	/// Returns the attendance entry by the calendar entry date.
	/// </summary>
	/// <param name="date">The attendance date to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Attendance.GetByDate)]
	[ProducesResponseType(typeof(AttendanceResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByDate(DateTime date, CancellationToken token)
	{
		ErrorOr<AttendanceResponse> result = await attendanceService
			.GetByDate(currentUserService.UserId, date, token)
			.ConfigureAwait(false);

		return Get(result);
	}

	/// <summary>
	/// Creates a new attendance entry
	/// </summary>
	/// <param name="request">The attendance create request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="201">The resource was successfully created.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="409">Conflict with the current state of the target resource.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPost(Endpoints.Attendance.Post)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Post([FromBody] AttendanceCreateRequest request, CancellationToken token)
	{
		ErrorOr<Created> result = await attendanceService
			.Create(currentUserService.UserId, request, token)
			.ConfigureAwait(false);

		return PostWithoutLocation(result);
	}

	/// <summary>
	/// Creates multiple new attendance entries.
	/// </summary>
	/// <param name="requests">The attendances create request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="201">The resource was successfully created.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="409">Conflict with the current state of the target resource.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPost(Endpoints.Attendance.PostMultiple)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> PostMultiple([FromBody] IEnumerable<AttendanceCreateRequest> requests, CancellationToken token)
	{
		ErrorOr<Created> result = await attendanceService
			.CreateMultiple(currentUserService.UserId, requests, token)
			.ConfigureAwait(false);

		return PostWithoutLocation(result);
	}

	/// <summary>
	/// Updates a existing attendance entry.
	/// </summary>
	/// <param name="request">The attendance update request to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The resource was successfully updated.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPut(Endpoints.Attendance.Put)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Put([FromBody] AttendanceUpdateRequest request, CancellationToken token)
	{
		ErrorOr<Updated> result = await attendanceService
			.Update(request, token)
			.ConfigureAwait(false);

		return Put(result);
	}

	/// <summary>
	/// Updates multiple existing attendance entries.
	/// </summary>
	/// <param name="requests">The attendance update requests to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The resource was successfully updated.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPut(Endpoints.Attendance.PutMultiple)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> PutMultiple([FromBody] IEnumerable<AttendanceUpdateRequest> requests, CancellationToken token)
	{
		ErrorOr<Updated> result = await attendanceService
			.UpdateMultiple(requests, token)
			.ConfigureAwait(false);

		return Put(result);
	}
}
