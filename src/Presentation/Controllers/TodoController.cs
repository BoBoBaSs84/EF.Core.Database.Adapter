using Application.Contracts.Requests.Todo;
using Application.Contracts.Responses.Todo;
using Application.Interfaces.Application.Todo;
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
	/// Deletes an existing todo item.
	/// </summary>
	/// <param name="itemId">The todo item identifier to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The resource was successfully deleted.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpDelete(Endpoints.Todo.DeleteItem)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> DeleteItem(Guid itemId, CancellationToken token = default)
	{
		ErrorOr<Deleted> response = await todoService
			.DeleteItemById(itemId, token)
			.ConfigureAwait(false);

		return Delete(response);
	}

	/// <summary>
	/// Deletes an existing todo list.
	/// </summary>
	/// <param name="listId">The todo list identifier to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The resource was successfully deleted.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpDelete(Endpoints.Todo.DeleteList)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> DeleteList(Guid listId, CancellationToken token = default)
	{
		ErrorOr<Deleted> response = await todoService
			.DeleteListById(listId, token)
			.ConfigureAwait(false);

		return Delete(response);
	}

	/// <summary>
	/// Returns a collection of todo lists for the current user.
	/// </summary>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Todo.GetAll)]
	[ProducesResponseType(typeof(IEnumerable<ListResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetAll(CancellationToken token = default)
	{
		ErrorOr<IEnumerable<ListResponse>> response = await todoService
			.GetListsByUserId(userService.UserId, token)
			.ConfigureAwait(false);

		return Get(response);
	}

	/// <summary>
	/// Returns a todo list for the current user by the provided <paramref name="listId"/>.
	/// </summary>
	/// <param name="listId">The todo list identifier to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Todo.GetById)]
	[ProducesResponseType(typeof(IEnumerable<ListResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetById(Guid listId, CancellationToken token = default)
	{
		ErrorOr<ListResponse> response = await todoService
			.GetListByListId(userService.UserId, listId, token)
			.ConfigureAwait(false);

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
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> PostList([FromBody] ListCreateRequest request, CancellationToken token = default)
	{
		ErrorOr<Created> response = await todoService
			.CreateListByUserId(userService.UserId, request, token)
			.ConfigureAwait(false);

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
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPost(Endpoints.Todo.PostItem)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> PostItem(Guid listId, [FromBody] ItemCreateRequest request, CancellationToken token = default)
	{
		ErrorOr<Created> response = await todoService
			.CreateItemByListId(listId, request, token)
			.ConfigureAwait(false);

		return PostWithoutLocation(response);
	}

	/// <summary>
	/// Updates an existing todo item for the provied <paramref name="itemId"/>.
	/// </summary>
	/// <param name="itemId">The todo item identifier to use.</param>
	/// <param name="request">The todo item update request to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The resource was successfully updated.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPut(Endpoints.Todo.PutItem)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> PutItem(Guid itemId, [FromBody] ItemUpdateRequest request, CancellationToken token = default)
	{
		ErrorOr<Updated> response = await todoService
			.UpdateItemById(itemId, request, token)
			.ConfigureAwait(false);

		return Put(response);
	}

	/// <summary>
	/// Updates an existing todo list for the provied <paramref name="listId"/>.
	/// </summary>
	/// <param name="listId">The todo list identifier to use.</param>
	/// <param name="request">The todo list update request to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The resource was successfully updated.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPut(Endpoints.Todo.PutList)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> PutList(Guid listId, [FromBody] ListUpdateRequest request, CancellationToken token = default)
	{
		ErrorOr<Updated> response = await todoService
			.UpdateListById(listId, request, token)
			.ConfigureAwait(false);

		return Put(response);
	}
}
