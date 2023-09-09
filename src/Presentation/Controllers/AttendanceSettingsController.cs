using Application.Contracts.Requests.Attendance;
using Application.Contracts.Responses.Attendance;
using Application.Interfaces.Application;
using Application.Interfaces.Presentation.Services;

using Domain.Errors;
using Domain.Results;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Presentation.Common;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

/// <summary>
/// The attendance settings controller class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
[Authorize]
[Route(Endpoints.Attendance.Settings.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class AttendanceSettingsController : ApiControllerBase
{
	private readonly IAttendanceSettingsService _attendanceSettingsService;
	private readonly ICurrentUserService _currentUserService;

	/// <summary>
	/// Initializes an instance of the attendance settings controller class.
	/// </summary>
	/// <param name="attendanceSettingsService">The attendance settings service to use.</param>
	/// <param name="currentUserService">The current user service to use.</param>
	public AttendanceSettingsController(IAttendanceSettingsService attendanceSettingsService, ICurrentUserService currentUserService)
	{
		_attendanceSettingsService = attendanceSettingsService;
		_currentUserService = currentUserService;
	}

	/// <summary>
	/// Returns the attendance settings for for the application user.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Attendance.Settings.Get)]
	[ProducesResponseType(typeof(AttendanceSettingsResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Get(CancellationToken cancellationToken)
	{
		ErrorOr<AttendanceSettingsResponse> result =
			await _attendanceSettingsService.Get(_currentUserService.UserId, false, cancellationToken);

		return Get(result);
	}

	/// <summary>
	/// Creates new attendance settings for for the application user.
	/// </summary>
	/// <param name="request">The attendance settings create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="201">If the attendances were created.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="409">If a record is already created.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpPost(Endpoints.Attendance.Settings.Post)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Post([FromBody] AttendanceSettingsRequest request, CancellationToken cancellationToken)
	{
		ErrorOr<Created> result =
			await _attendanceSettingsService.Create(_currentUserService.UserId, request, cancellationToken);

		return PostWithoutLocation(result);
	}

	/// <summary>
	/// Updates existing attendance settings for for the application user.
	/// </summary>
	/// <param name="request">The attendance settings update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">If the attendances were updated.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpPut(Endpoints.Attendance.Settings.Put)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Put([FromBody] AttendanceSettingsRequest request, CancellationToken cancellationToken)
	{
		ErrorOr<Updated> result =
			await _attendanceSettingsService.Update(_currentUserService.UserId, request, cancellationToken);

		return Put(result);
	}
}
