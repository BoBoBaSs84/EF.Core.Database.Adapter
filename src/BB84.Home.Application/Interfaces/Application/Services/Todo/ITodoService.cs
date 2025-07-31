using BB84.Home.Application.Contracts.Requests.Todo;
using BB84.Home.Application.Contracts.Responses.Todo;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

namespace BB84.Home.Application.Interfaces.Application.Services.Todo;

/// <summary>
/// The interface fo the todo service
/// </summary>
public interface ITodoService
{
	/// <summary>
	/// Creates a new todo list with the provided <paramref name="request"/>.
	/// </summary>
	/// <param name="request">The create todo list request to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> CreateListAsync(ListCreateRequest request, CancellationToken token = default);

	/// <summary>
	/// Creates a new todo item for the provided <paramref name="listId"/> with the provided <paramref name="request"/>.
	/// </summary>
	/// <param name="listId">The list identifier to use.</param>
	/// <param name="request">The create todo item request to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> CreateItemAsync(Guid listId, ItemCreateRequest request, CancellationToken token = default);

	/// <summary>
	/// Deletes an existing todo list by the provided <paramref name="listId"/>.
	/// </summary>
	/// <param name="listId">The list identifier to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> DeleteListAsync(Guid listId, CancellationToken token = default);

	/// <summary>
	/// Deletes an existing todo item by the provided <paramref name="itemId"/>.
	/// </summary>
	/// <param name="itemId">The item identifier to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> DeleteItemAsync(Guid itemId, CancellationToken token = default);

	/// <summary>
	/// Returns a collection of all todo lists.
	/// </summary>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IEnumerable<ListResponse>>> GetAllListsAsync(CancellationToken token = default);

	/// <summary>
	/// Returns a todo list with its items for the provided <paramref name="listId"/>.
	/// </summary>
	/// <param name="listId">The list identifier to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<ListResponse>> GetListAsync(Guid listId, CancellationToken token = default);

	/// <summary>
	/// Updates an existing todo list by the provided <paramref name="listId"/> with the provided
	/// update <paramref name="request"/>.
	/// </summary>
	/// <param name="listId">The list identifier to use.</param>
	/// <param name="request">The update todo list request to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> UpdateListAsync(Guid listId, ListUpdateRequest request, CancellationToken token = default);

	/// <summary>
	/// Updates an existing todo item by the provided <paramref name="itemId"/> with the provided
	/// update <paramref name="request"/>.
	/// </summary>
	/// <param name="itemId">The item identifier to use.</param>
	/// <param name="request">The update todo list request to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> UpdateItemAsync(Guid itemId, ItemUpdateRequest request, CancellationToken token = default);
}
