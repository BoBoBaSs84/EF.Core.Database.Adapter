using Application.Contracts.Responses.Enumerators;
using Application.Interfaces.Application.Common;

using Asp.Versioning;

using Domain.Enumerators;
using Domain.Errors;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Presentation.Attributes;
using Presentation.Common;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

/// <summary>
/// The enumerator controller class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
/// <param name="enumeratorService">The enumerator service to use.</param>
[Route(Endpoints.Enumerator.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class EnumeratorController(IEnumeratorService enumeratorService) : ApiControllerBase
{
	private readonly IEnumeratorService _enumeratorService = enumeratorService;

	/// <summary>
	/// Returns all card type enumerators.
	/// </summary>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Enumerator.CardType.Get)]
	[ProducesResponseType(typeof(IEnumerable<CardTypeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public IActionResult GetCardTypes()
	{
		ErrorOr<IEnumerable<CardTypeResponse>> result = _enumeratorService.GetCardTypes();
		return Get(result);
	}

	/// <summary>
	/// Returns all attendance type enumerators.
	/// </summary>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Enumerator.AttendanceType.Get)]
	[ProducesResponseType(typeof(IEnumerable<AttendanceTypeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public IActionResult GetDayTypes()
	{
		ErrorOr<IEnumerable<AttendanceTypeResponse>> result = _enumeratorService.GetAttendanceTypes();
		return Get(result);
	}

	/// <summary>
	/// Returns all priority level type enumerators.
	/// </summary>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Enumerator.PriorityLevelType.Get)]
	[ProducesResponseType(typeof(IEnumerable<AttendanceTypeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public IActionResult GetPriorityLevelTypes()
	{
		ErrorOr<IEnumerable<PriorityLevelTypeResponse>> result = _enumeratorService.GetPriorityLevelTypes();
		return Get(result);
	}

	/// <summary>
	/// Returns all role type enumerators.
	/// </summary>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Enumerator.RoleType.Get), AuthorizeRoles(RoleType.ADMINISTRATOR)]
	[ProducesResponseType(typeof(IEnumerable<RoleTypeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public IActionResult GetRoleTypes()
	{
		ErrorOr<IEnumerable<RoleTypeResponse>> result = _enumeratorService.GetRoleTypes();
		return Get(result);
	}

	/// <summary>
	/// Returns all work day type enumerators.
	/// </summary>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Enumerator.WorkDayType.Get)]
	[ProducesResponseType(typeof(IEnumerable<WorkDayTypeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public IActionResult GetWorkDayTypes()
	{
		ErrorOr<IEnumerable<WorkDayTypeResponse>> result = _enumeratorService.GetWorkDayTypes();
		return Get(result);
	}
}
