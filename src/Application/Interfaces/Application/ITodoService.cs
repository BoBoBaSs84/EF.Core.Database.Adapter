using Application.Contracts.Requests.Todo;
using Application.Contracts.Responses.Todo;

using Domain.Errors;
using Domain.Results;

namespace Application.Interfaces.Application;

/// <summary>
/// The interface fo the todo service
/// </summary>
public interface ITodoService
{
	/// <summary>
	/// Creates a new todo list for the provided <paramref name="userId"/>.
	/// </summary>
	/// <param name="userId">The user identifier to use.</param>
	/// <param name="request">The create todo list request to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> CreateListByUserId(Guid userId, ListCreateRequest request, CancellationToken token = default);

	/// <summary>
	/// Creates a new todo item for the provided <paramref name="listId"/>.
	/// </summary>
	/// <param name="listId">The list identifier to use.</param>
	/// <param name="request">The create todo item request to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> CreateItemByListId(Guid listId, ItemCreateRequest request, CancellationToken token = default);

	/// <summary>
	/// Returns a collection of todo lists for the provided <paramref name="userId"/>.
	/// </summary>
	/// <param name="userId">The user identifier to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns>A collection of todo lists.</returns>
	Task<ErrorOr<IEnumerable<ListResponse>>> GetListsByUserId(Guid userId, CancellationToken token = default);

	/// <summary>
	/// Returns a todo list with items for the provided <paramref name="userId"/> an list <paramref name="listId"/>.
	/// </summary>
	/// <param name="userId">The user identifier to use.</param>
	/// <param name="listId">The list identifier to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns>A todo list.</returns>
	Task<ErrorOr<ListResponse>> GetListByListId(Guid userId, Guid listId, CancellationToken token = default);
}
