using Application.Contracts.Responses.Identity;
using Application.Contracts.Requests.Identity;
using Application.Interfaces.Application;
using Domain.Errors;
using Domain.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Presentation.Common;
using Presentation.Controllers.Base;
using Roles = Domain.Enumerators.RoleTypes;
using Application.Interfaces.Infrastructure.Services;

namespace Presentation.Controllers;

/// <summary>
/// The <see cref="UserManagementController"/> class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
[Route(Endpoints.UserManagement.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
[AuthorizeRoles(Roles.ADMINISTRATOR, Roles.SUPERUSER, Roles.USER)]
public sealed class UserManagementController : ApiControllerBase
{
	private readonly IAuthenticationService _authenticationService;
	private readonly ICurrentUserService _currentUserService;

	/// <summary>
	/// Initializes an instance of <see cref="UserManagementController"/> class.
	/// </summary>
	/// <param name="authenticationService">The authentication service.</param>
	/// <param name="currentUserService">The current user service.</param>
	public UserManagementController(IAuthenticationService authenticationService, ICurrentUserService currentUserService)
	{
		_authenticationService = authenticationService;
		_currentUserService = currentUserService;
	}

	/// <summary>
	/// Should add a user to a certain role.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="roleName">The role the user should be added to.</param>
	/// <response code="200">If the user was added to the role.</response>
	/// <response code="400">If something is wrong with the request.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the user or the role was not found.</response>
	/// <response code="500">If the something went wrong.</response>
	[AuthorizeRoles(Roles.ADMINISTRATOR)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	[HttpPost(Endpoints.UserManagement.AddUserToRole)]
	public async Task<IActionResult> AddUserToRole(int userId, string roleName)
	{
		ErrorOr<Created> result =
			await _authenticationService.AddUserToRole(userId, roleName);

		return Get(result);
	}

	/// <summary>
	/// Should create a new application user.
	/// </summary>
	/// <param name="createRequest">The user create request.</param>
	/// <response code="201">If the new user was created.</response>
	/// <response code="400">If something is wrong with the request.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="500">If the something went wrong.</response>
	[AuthorizeRoles(Roles.ADMINISTRATOR)]
	[HttpPost(Endpoints.UserManagement.Create)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Create([FromBody] UserCreateRequest createRequest)
	{
		ErrorOr<Created> result =
			await _authenticationService.CreateUser(createRequest);

		return PostWithoutLocation(result);
	}

	/// <summary>
	/// Should retrieve all apllication users.
	/// </summary>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="500">If the something went wrong.</response>
	[AuthorizeRoles(Roles.ADMINISTRATOR)]
	[HttpGet(Endpoints.UserManagement.GetAll)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetAll()
	{
		ErrorOr<IEnumerable<UserResponse>> result =
			await _authenticationService.GetAll();

		return Get(result);
	}


	/// <summary>
	/// Should return the application user by its user name.
	/// </summary>
	/// <param name="userName">The user name of the user.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the user was not found.</response>
	/// <response code="500">If the something went wrong.</response>
	[AuthorizeRoles(Roles.ADMINISTRATOR)]
	[HttpGet(Endpoints.UserManagement.GetByName)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByName(string userName)
	{
		ErrorOr<UserResponse> result =
			await _authenticationService.GetUserByName(userName);

		return Get(result);
	}

	/// <summary>
	/// Should return the current application user.
	/// </summary>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="500">If the something went wrong.</response>
	[HttpGet(Endpoints.UserManagement.GetCurrent)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetCurrent()
	{
		ErrorOr<UserResponse> result =
			await _authenticationService.GetUserById(_currentUserService.UserId);

		return Get(result);
	}

	/// <summary>
	/// Should remove a user from a certain role.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="roleName">The role the user should be added to.</param>
	/// <response code="200">If the user was added to the role.</response>
	/// <response code="400">If something is wrong with the request.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the user or the role was not found.</response>
	/// <response code="500">If the something went wrong.</response>
	[AuthorizeRoles(Roles.ADMINISTRATOR)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	[HttpDelete(Endpoints.UserManagement.RemoveUserToRole)]
	public async Task<IActionResult> RemoveUserToRole(int userId, string roleName)
	{
		ErrorOr<Deleted> result =
			await _authenticationService.RemoveUserToRole(userId, roleName);

		return Get(result);
	}

	/// <summary>
	/// Should update the current application user.
	/// </summary>
	/// <param name="updateRequest">The user update request.</param>
	/// <response code="200">If the user was updated.</response>
	/// <response code="400">If something is wrong with the request.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the user to update was not found.</response>
	/// <response code="500">If the something went wrong.</response>
	[HttpPut(Endpoints.UserManagement.UpdateCurrent)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateCurrent([FromBody] UserUpdateRequest updateRequest)
	{
		ErrorOr<Updated> result =
			await _authenticationService.UpdateUser(_currentUserService.UserId, updateRequest);

		return Get(result);
	}
}
