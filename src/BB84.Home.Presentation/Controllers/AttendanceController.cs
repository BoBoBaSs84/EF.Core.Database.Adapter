using Asp.Versioning;

using BB84.Home.Application.Contracts.Requests.Attendance;
using BB84.Home.Application.Contracts.Responses.Attendance;
using BB84.Home.Application.Features.Requests;
using BB84.Home.Application.Features.Responses;
using BB84.Home.Application.Interfaces.Application.Services.Attendance;
using BB84.Home.Application.Interfaces.Presentation.Services;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;
using BB84.Home.Presentation.Common;
using BB84.Home.Presentation.Controllers.Base;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BB84.Home.Presentation.Controllers;

/// <summary>
/// Provides endpoints for managing attendance records, including creation, retrieval, updating, and deletion.
/// </summary>
/// <remarks>
/// This controller is responsible for handling attendance-related operations for the application user.
/// It includes methods for managing individual and multiple attendance entries, as well as retrieving
/// attendance data based on specific criteria such as date or query parameters.
/// </remarks>
/// <param name="attendanceService">The service for managing attendance records.</param>
/// <param name="userService">The service for accessing the current user's information.</param>
[Authorize]
[Route(Endpoints.Attendance.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class AttendanceController(IAttendanceService attendanceService, ICurrentUserService userService) : ApiControllerBase
{
	/// <summary>
	/// Deletes an attendance record by its unique identifier.
	/// </summary>
	/// <remarks>
	/// If the specified attendance record does not exist, an error is returned.
	/// In the event of an exception during the operation, an error is logged and returned.
	/// </remarks>
	/// <param name="id">The unique identifier of the attendance record to delete.</param>
	/// <param name="token">An optional <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <response code="200">The resource was successfully deleted.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">The user is not allowed to access the resource.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpDelete(Endpoints.Attendance.DeleteById)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> DeleteById(Guid id, CancellationToken token)
	{
		ErrorOr<Deleted> result = await attendanceService
			.DeleteById(id, token)
			.ConfigureAwait(false);

		return Delete(result);
	}

	/// <summary>
	/// Deletes attendance records corresponding to the specified IDs.
	/// </summary>
	/// <remarks>
	/// This method retrieves the attendance records associated with the provided IDs and deletes them.
	/// If no records are found for the given IDs, an error is returned.
	/// The operation is transactional,  ensuring that changes are committed only if the deletion succeeds.
	/// </remarks>
	/// <param name="ids">A collection of unique identifiers representing the attendance records to delete.</param>
	/// <param name="token">An optional <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <response code="200">The resource was successfully deleted.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">The user is not allowed to access the resource.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpDelete(Endpoints.Attendance.DeleteByIds)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> DeleteByIds([FromBody] IEnumerable<Guid> ids, CancellationToken token)
	{
		ErrorOr<Deleted> result = await attendanceService
			.DeleteByIds(ids, token)
			.ConfigureAwait(false);

		return Delete(result);
	}

	/// <summary>
	/// Retrieves a paginated list of attendance records for a specified user based on the provided filtering parameters.
	/// </summary>
	/// <remarks>
	/// This method retrieves attendance records for the specified user, applying the provided filters and pagination
	/// settings. The results are ordered by the attendance date in ascending order. If the operation fails, an error
	/// is returned instead of the paginated list.
	/// </remarks>
	/// <param name="parameters">
	/// The filtering and pagination parameters used to refine the attendance records.
	/// This includes page number, page size, and any additional filters.
	/// </param>
	/// <param name="token">A cancellation token that can be used to cancel the operation.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">The user is not allowed to access the resource.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Attendance.GetPagedByParameters)]
	[ProducesResponseType(typeof(IPagedList<AttendanceResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetPagedByParameters([FromQuery] AttendanceParameters parameters, CancellationToken token)
	{
		ErrorOr<IPagedList<AttendanceResponse>> result = await attendanceService
			.GetPagedByParameters(userService.UserId, parameters, token)
			.ConfigureAwait(false);

		return Get(result, result.Value.MetaData);
	}

	/// <summary>
	/// Retrieves the attendance record for a specific user on a given date.
	/// </summary>
	/// <remarks>
	/// This method queries the attendance repository for a record matching the specified user ID and date.
	/// If the record is found, it is mapped to an <see cref="AttendanceResponse"/> and returned.
	/// If no record is found, an error is returned.
	/// </remarks>
	/// <param name="date">The date for which the attendance record is requested. Only the date component is considered.</param>
	/// <param name="token">An optional <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">The user is not allowed to access the resource.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Attendance.GetByDate)]
	[ProducesResponseType(typeof(AttendanceResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByDate(DateTime date, CancellationToken token)
	{
		ErrorOr<AttendanceResponse> result = await attendanceService
			.GetByUserIdAndDate(userService.UserId, date, token)
			.ConfigureAwait(false);

		return Get(result);
	}

	/// <summary>
	/// Creates a new attendance record for the specified user.
	/// </summary>
	/// <remarks>
	/// If an attendance record already exists for the specified user and date, the operation will fail with a conflict error.
	/// </remarks>
	/// <param name="request">The details of the attendance record to be created, including the date and other relevant information.</param>
	/// <param name="token">A cancellation token that can be used to cancel the operation.</param>
	/// <response code="201">The resource was successfully created.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">The user is not allowed to access the resource.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="409">Conflict with the current state of the target resource.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPost(Endpoints.Attendance.Post)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Post([FromBody] AttendanceCreateRequest request, CancellationToken token)
	{
		ErrorOr<Created> result = await attendanceService
			.CreateByUserId(userService.UserId, request, token)
			.ConfigureAwait(false);

		return PostWithoutLocation(result);
	}

	/// <summary>
	/// Creates multiple attendance records for a specified user.
	/// </summary>
	/// <remarks>
	/// This method ensures that no duplicate attendance records are created for the specified user and dates.
	/// If any attendance records already exist for the given dates, the operation will fail with a conflict error.
	/// </remarks>
	/// <param name="requests">A collection of <see cref="AttendanceCreateRequest"/> objects representing the attendance records to be created.</param>
	/// <param name="token">An optional <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <response code="201">The resource was successfully created.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">The user is not allowed to access the resource.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="409">Conflict with the current state of the target resource.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPost(Endpoints.Attendance.PostMultiple)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> PostMultiple([FromBody] IEnumerable<AttendanceCreateRequest> requests, CancellationToken token)
	{
		ErrorOr<Created> result = await attendanceService
			.CreateMultipleByUserId(userService.UserId, requests, token)
			.ConfigureAwait(false);

		return PostWithoutLocation(result);
	}

	/// <summary>
	/// Updates an existing attendance record with the provided data.
	/// </summary>
	/// <remarks>
	/// This method performs the update operation by mapping the provided request data to the existing attendance record.
	/// Changes are committed to the repository, and any errors during the process are logged.
	/// </remarks>
	/// <param name="request">The request containing the updated attendance data.</param>
	/// <param name="token">A cancellation token that can be used to cancel the operation.</param>
	/// <response code="200">The resource was successfully updated.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">The user is not allowed to access the resource.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPut(Endpoints.Attendance.Put)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
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
	/// Updates multiple attendance records based on the provided update requests.
	/// </summary>
	/// <remarks>
	/// This method attempts to update multiple attendance records in a single operation.
	/// If any of the specified records are not found, an error is returned.
	/// The method uses the provided update requests to map new values onto the corresponding attendance entities.
	/// Changes are committed to the database upon successful completion.
	/// </remarks>
	/// <param name="requests">
	/// A collection of <see cref="AttendanceUpdateRequest"/> objects containing the updated data for each attendance record.
	/// </param>
	/// <param name="token">An optional <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <response code="200">The resource was successfully updated.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">The user is not allowed to access the resource.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPut(Endpoints.Attendance.PutMultiple)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
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
