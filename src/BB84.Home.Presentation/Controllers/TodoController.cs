using Asp.Versioning;

using BB84.Home.Application.Contracts.Requests.Todo;
using BB84.Home.Application.Contracts.Responses.Todo;
using BB84.Home.Application.Interfaces.Application.Services.Todo;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;
using BB84.Home.Presentation.Common;
using BB84.Home.Presentation.Controllers.Base;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BB84.Home.Presentation.Controllers;

/// <summary>
/// Provides functionality for managing todo list and item records, including creation,
/// retrieval, updating, and deletion.
/// </summary>
/// <param name="todoService">The todo service instance to use.</param>
[Authorize]
[Route(Endpoints.Todo.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class TodoController(ITodoService todoService) : ApiControllerBase
{
	/// <summary>
	/// Deletes a to-do item identified by its unique identifier.
	/// </summary>
	/// <remarks>
	/// This method attempts to delete the specified item from the repository. If the item does
	/// not exist, an error is returned. If the deletion succeeds, the changes are committed to
	/// the repository.
	/// </remarks>
	/// <param name="itemId">The unique identifier of the item to delete.</param>
	/// <param name="token">A cancellation token that can be used to cancel the operation.</param>	
	/// <response code="200">The resource was successfully deleted.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">Insufficient permissions to access the resource or action.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpDelete(Endpoints.Todo.DeleteItem)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> DeleteItem(Guid itemId, CancellationToken token = default)
	{
		ErrorOr<Deleted> response = await todoService
			.DeleteItemAsync(itemId, token)
			.ConfigureAwait(false);

		return Delete(response);
	}

	/// <summary>
	/// Deletes a to-do list identified by its unique ID.
	/// </summary>
	/// <remarks>
	/// This method retrieves the to-do list by its ID, deletes it from the repository, and commits the
	/// changes. If the to-do list does not exist, an error is returned. If an exception occurs during
	/// the operation, an error is logged and returned.
	/// </remarks>
	/// <param name="listId">The unique identifier of the to-do list to delete.</param>
	/// <param name="token">A cancellation token that can be used to cancel the operation.</param>
	/// <response code="200">The resource was successfully deleted.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">Insufficient permissions to access the resource or action.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpDelete(Endpoints.Todo.DeleteList)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> DeleteList(Guid listId, CancellationToken token = default)
	{
		ErrorOr<Deleted> response = await todoService
			.DeleteListAsync(listId, token)
			.ConfigureAwait(false);

		return Delete(response);
	}

	/// <summary>
	/// Retrieves all to-do lists asynchronously.
	/// </summary>
	/// <remarks>
	/// This method fetches all to-do lists from the repository and maps them to response
	/// objects. If an error occurs during the operation, an appropriate error is returned.
	/// </remarks>
	/// <param name="token">A cancellation token that can be used to cancel the operation.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">Insufficient permissions to access the resource or action.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Todo.GetAllLists)]
	[ProducesResponseType(typeof(IEnumerable<ListResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetAllLists(CancellationToken token = default)
	{
		ErrorOr<IEnumerable<ListResponse>> response = await todoService
			.GetAllListsAsync(token)
			.ConfigureAwait(false);

		return Get(response);
	}

	/// <summary>
	/// Retrieves a to-do list by its unique identifier.
	/// </summary>
	/// <remarks>
	/// This method attempts to retrieve a to-do list from the repository, including its
	/// associated items. If the list is not found, an error is returned.
	/// </remarks>
	/// <param name="listId">The unique identifier of the to-do list to retrieve.</param>
	/// <param name="token">A cancellation token that can be used to cancel the operation.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">Insufficient permissions to access the resource or action.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Todo.GetList)]
	[ProducesResponseType(typeof(ListResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetList(Guid listId, CancellationToken token = default)
	{
		ErrorOr<ListResponse> response = await todoService
			.GetListAsync(listId, token)
			.ConfigureAwait(false);

		return Get(response);
	}

	/// <summary>
	/// Creates a new to-do list based on the provided request.
	/// </summary>
	/// <remarks>
	/// This method maps the provided request to a list entity, associates it with the current user,
	/// and persists it to the repository. If the operation fails, an error is returned.
	/// </remarks>
	/// <param name="request">The request containing the details of the to-do list to be created.</param>
	/// <param name="token">A cancellation token that can be used to cancel the operation.</param>
	/// <response code="201">The resource was successfully created.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">Insufficient permissions to access the resource or action.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPost(Endpoints.Todo.CreateList)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> CreateList([FromBody] ListCreateRequest request, CancellationToken token = default)
	{
		ErrorOr<Created> response = await todoService
			.CreateListAsync(request, token)
			.ConfigureAwait(false);

		return PostWithoutLocation(response);
	}

	/// <summary>
	/// Creates a new item within the specified to-do list.
	/// </summary>
	/// <remarks>
	/// This method attempts to create a new item in the specified to-do list. If the list does not exist, 
	/// an error is returned. If the operation fails due to an exception, an appropriate error is logged
	/// and returned.
	/// </remarks>
	/// <param name="listId">The unique identifier of the to-do list where the item will be created.</param>
	/// <param name="request">The details of the item to be created, including its name and other properties.</param>
	/// <param name="token">A cancellation token that can be used to cancel the operation.</param>
	/// <response code="201">The resource was successfully created.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">Insufficient permissions to access the resource or action.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPost(Endpoints.Todo.CreateItem)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> CreateItem(Guid listId, [FromBody] ItemCreateRequest request, CancellationToken token = default)
	{
		ErrorOr<Created> response = await todoService
			.CreateItemAsync(listId, request, token)
			.ConfigureAwait(false);

		return PostWithoutLocation(response);
	}

	/// <summary>
	/// Updates an existing to-do item with the specified changes.
	/// </summary>
	/// <remarks>
	/// This method retrieves the to-do item by its identifier, applies the updates specified in the
	/// <paramref name="request"/>, and commits the changes to the repository. If the to-do item does
	/// not exist, an error is returned.
	/// </remarks>
	/// <param name="itemId">The unique identifier of the to-do item to update.</param>
	/// <param name="request">The object containing the updated properties for the to-do item.</param>
	/// <param name="token">A cancellation token that can be used to cancel the operation.</param>
	/// <response code="200">The resource was successfully updated.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">Insufficient permissions to access the resource or action.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPut(Endpoints.Todo.UpdateItem)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateItem(Guid itemId, [FromBody] ItemUpdateRequest request, CancellationToken token = default)
	{
		ErrorOr<Updated> response = await todoService
			.UpdateItemAsync(itemId, request, token)
			.ConfigureAwait(false);

		return Put(response);
	}

	/// <summary>
	/// Updates an existing to-do list with the specified changes.
	/// </summary>
	/// <remarks>
	/// This method retrieves the to-do list by its identifier, applies the updates specified in the
	/// <paramref name="request"/>, and commits the changes to the repository. If the to-do list does
	/// not exist, an error is returned.
	/// </remarks>
	/// <param name="listId">The unique identifier of the to-do list to update.</param>
	/// <param name="request">The object containing the updated properties for the to-do list.</param>
	/// <param name="token">A cancellation token that can be used to cancel the operation.</param>
	/// <response code="200">The resource was successfully updated.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">Insufficient permissions to access the resource or action.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPut(Endpoints.Todo.UpdateList)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateList(Guid listId, [FromBody] ListUpdateRequest request, CancellationToken token = default)
	{
		ErrorOr<Updated> response = await todoService
			.UpdateListAsync(listId, request, token)
			.ConfigureAwait(false);

		return Put(response);
	}
}
