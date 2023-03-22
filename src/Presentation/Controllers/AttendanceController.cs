using Application.Contracts.Responses;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Identity;
using Domain.Errors;
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
	/// Initializes an instance of <see cref="CalendarDayController"/> class.
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
	public async Task<IActionResult> GetPagedByParameters([FromQuery] AttendanceParameters parameters, CancellationToken cancellationToken = default)
	{
		ErrorOr<IPagedList<AttendanceResponse>> result =
			await _attendanceService.GetPagedByParameters(_currentUserService.UserId, parameters, false, cancellationToken);
		return Get(result, result.Value?.MetaData);
	}
}
