using Application.Contracts.Requests.Todo;
using Application.Contracts.Responses.Todo;
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
/// The todo list and item api controller.
/// </summary>
/// <param name="todoService">The todo service instance to use.</param>
/// <param name="userService">The current user service instance to use.</param>
[Authorize]
[Route(Endpoints.Todo.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class TodoController(ITodoService todoService, ICurrentUserService userService) : ApiControllerBase
{
	/// <summary>
	/// Returns a collection of todo lists for the current user.
	/// </summary>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Todo.GetAll)]
	[ProducesResponseType(typeof(IEnumerable<ListResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetAll(CancellationToken token = default)
	{
		ErrorOr<IEnumerable<ListResponse>> response =
			await todoService.GetListsByUserId(userService.UserId, token);

		return Get(response);
	}

	/// <summary>
	/// Returns a todo list for the current user by the provided <paramref name="listId"/>.
	/// </summary>
	/// <param name="listId">The todo list identifier to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Todo.GetById)]
	[ProducesResponseType(typeof(IEnumerable<ListResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetById(Guid listId, CancellationToken token = default)
	{
		ErrorOr<ListResponse> response =
			await todoService.GetListByListId(userService.UserId, listId, token);

		return Get(response);
	}

	/// <summary>
	/// Creates a new todo list for the current user.
	/// </summary>
	/// <param name="request">The list create request to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="201">The resource was successfully created.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPost(Endpoints.Todo.PostList)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> PostList([FromBody] ListCreateRequest request, CancellationToken token = default)
	{
		ErrorOr<Created> response =
			await todoService.CreateListByUserId(userService.UserId, request, token);

		return PostWithoutLocation(response);
	}

	/// <summary>
	/// Creates a new todo item for the provied <paramref name="listId"/>.
	/// </summary>
	/// <param name="listId">The todo list identifier to use.</param>
	/// <param name="request">The item create request to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="201">The resource was successfully created.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPost(Endpoints.Todo.PostItem)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> PostItem(Guid listId, [FromBody] ItemCreateRequest request, CancellationToken token = default)
	{
		ErrorOr<Created> response =
			await todoService.CreateItemByListId(listId, request, token);

		return PostWithoutLocation(response);
	}
}
