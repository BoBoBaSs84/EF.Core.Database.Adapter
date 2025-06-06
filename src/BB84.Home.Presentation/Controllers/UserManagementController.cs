﻿using Asp.Versioning;

using BB84.Home.Application.Contracts.Requests.Identity;
using BB84.Home.Application.Contracts.Responses.Identity;
using BB84.Home.Application.Interfaces.Application.Services.Identity;
using BB84.Home.Application.Interfaces.Presentation.Services;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;
using BB84.Home.Presentation.Attributes;
using BB84.Home.Presentation.Common;
using BB84.Home.Presentation.Controllers.Base;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Roles = BB84.Home.Domain.Enumerators.RoleType;

namespace BB84.Home.Presentation.Controllers;

/// <summary>
/// The <see cref="UserManagementController"/> class.
/// </summary>
/// <param name="authenticationService">The authentication service.</param>
/// <param name="userService">The current user service.</param>
[Route(Endpoints.UserManagement.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class UserManagementController(IAuthenticationService authenticationService, ICurrentUserService userService) : ApiControllerBase
{
	/// <summary>
	/// Adds the user with the <paramref name="userId"/> to the role with the <paramref name="roleId"/>.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="roleId">The identifier of the role.</param>
	/// <response code="200">If the user was added to the role.</response>
	/// <response code="400">If something is wrong with the request.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the user or the role was not found.</response>
	/// <response code="500">If the something went wrong.</response>
	[AuthorizeRoles(Roles.Administrator)]
	[HttpPost(Endpoints.UserManagement.AddUserToRole)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> AddUserToRole(Guid userId, Guid roleId)
	{
		ErrorOr<Created> result = await authenticationService
			.AddUserToRole(userId, roleId)
			.ConfigureAwait(false);

		return Put(result);
	}

	/// <summary>
	/// Creates a new application user.
	/// </summary>
	/// <param name="createRequest">The user create request.</param>
	/// <response code="201">If the new user was created.</response>
	/// <response code="400">If something is wrong with the request.</response>
	/// <response code="500">If the something went wrong.</response>
	[HttpPost(Endpoints.UserManagement.Create)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Create([FromBody] UserCreateRequest createRequest)
	{
		ErrorOr<Created> result = await authenticationService
			.CreateUser(createRequest)
			.ConfigureAwait(false);

		return PostWithoutLocation(result);
	}

	/// <summary>
	/// Returns all apllication users.
	/// </summary>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="500">If the something went wrong.</response>
	[AuthorizeRoles(Roles.Administrator)]
	[HttpGet(Endpoints.UserManagement.GetAll)]
	[ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetAll()
	{
		ErrorOr<IEnumerable<UserResponse>> result = await authenticationService
			.GetAllUser()
			.ConfigureAwait(false);

		return Get(result);
	}


	/// <summary>
	/// Returns the application user by its user name.
	/// </summary>
	/// <param name="userName">The user name of the user.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the user was not found.</response>
	/// <response code="500">If the something went wrong.</response>
	[AuthorizeRoles(Roles.Administrator)]
	[HttpGet(Endpoints.UserManagement.GetByName)]
	[ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByName(string userName)
	{
		ErrorOr<UserResponse> result = await authenticationService
			.GetUserByName(userName)
			.ConfigureAwait(false);

		return Get(result);
	}

	/// <summary>
	/// Returns the current application user.
	/// </summary>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="500">If the something went wrong.</response>
	[Authorize]
	[HttpGet(Endpoints.UserManagement.GetCurrent)]
	[ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetCurrent()
	{
		ErrorOr<UserResponse> result = await authenticationService
			.GetUserById(userService.UserId)
			.ConfigureAwait(false);

		return Get(result);
	}

	/// <summary>
	/// Removes the user with the <paramref name="userId"/> to the role with the <paramref name="roleId"/>.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="roleId">The identifier of the role.</param>
	/// <response code="200">If the user was added to the role.</response>
	/// <response code="400">If something is wrong with the request.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the user or the role was not found.</response>
	/// <response code="500">If the something went wrong.</response>
	[AuthorizeRoles(Roles.Administrator)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	[HttpDelete(Endpoints.UserManagement.RemoveUserToRole)]
	public async Task<IActionResult> RemoveUserFromRole(Guid userId, Guid roleId)
	{
		ErrorOr<Deleted> result = await authenticationService
			.RemoveUserFromRole(userId, roleId)
			.ConfigureAwait(false);

		return Delete(result);
	}

	/// <summary>
	/// Updates the current application user.
	/// </summary>
	/// <param name="updateRequest">The user update request.</param>
	/// <response code="200">If the user was updated.</response>
	/// <response code="400">If something is wrong with the request.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the user to update was not found.</response>
	/// <response code="500">If the something went wrong.</response>
	[Authorize]
	[HttpPut(Endpoints.UserManagement.UpdateCurrent)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateCurrent([FromBody] UserUpdateRequest updateRequest)
	{
		ErrorOr<Updated> result = await authenticationService
			.UpdateUser(userService.UserId, updateRequest)
			.ConfigureAwait(false);

		return Put(result);
	}
}
