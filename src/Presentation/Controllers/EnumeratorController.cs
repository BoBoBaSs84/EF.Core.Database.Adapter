using Application.Contracts.Responses.Enumerators;
using Application.Interfaces.Application;

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
[Route(Endpoints.Enumerator.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class EnumeratorController : ApiControllerBase
{
	private readonly IEnumeratorService _enumeratorService;

	/// <summary>
	/// Initializes an instance of the enumerator controller class.
	/// </summary>
	/// <param name="enumeratorService">The enumerator service to use.</param>
	public EnumeratorController(IEnumeratorService enumeratorService)
		=> _enumeratorService = enumeratorService;

	/// <summary>
	/// Returns all card type enumerators.
	/// </summary>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Enumerator.CardType.GetAll)]
	[ProducesResponseType(typeof(IEnumerable<CardTypeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public IActionResult GetCardTypes()
	{
		ErrorOr<IEnumerable<CardTypeResponse>> result = _enumeratorService.GetCardTypes();
		return Get(result);
	}

	/// <summary>
	/// Returns all day type enumerators.
	/// </summary>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Enumerator.DayType.GetAll)]
	[ProducesResponseType(typeof(IEnumerable<DayTypeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public IActionResult GetDayTypes()
	{
		ErrorOr<IEnumerable<DayTypeResponse>> result = _enumeratorService.GetDayTypes();
		return Get(result);
	}

	/// <summary>
	/// Returns all role type enumerators.
	/// </summary>
	/// <response code="200">If the result is returned.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.Enumerator.RoleType.GetAll), AuthorizeRoles(RoleType.ADMINISTRATOR)]
	[ProducesResponseType(typeof(IEnumerable<RoleTypeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public IActionResult GetRoleTypes()
	{
		ErrorOr<IEnumerable<RoleTypeResponse>> result = _enumeratorService.GetRoleTypes();
		return Get(result);
	}
}
