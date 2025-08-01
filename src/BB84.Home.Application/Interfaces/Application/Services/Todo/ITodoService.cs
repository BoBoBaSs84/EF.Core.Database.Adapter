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
	/// Creates a new to-do list based on the provided request.
	/// </summary>
	/// <remarks>
	/// This method maps the provided request to a list entity, associates it with the current user,
	/// and persists it to the repository. If the operation fails, an error is returned.
	/// </remarks>
	/// <param name="request">The request containing the details of the to-do list to be created.</param>
	/// <param name="token">A cancellation token that can be used to cancel the operation.</param>
	/// <returns>
	/// An <see cref="ErrorOr{T}"/> result containing <see langword="Created"/> if the operation succeeds.
	/// If the operation fails, the result contains an error indicating the reason for failure.
	/// </returns>
	Task<ErrorOr<Created>> CreateListAsync(ListCreateRequest request, CancellationToken token = default);

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
	/// <returns>
	/// An <see cref="ErrorOr{T}"/> result containing <see cref="Created"/> if the operation succeeds.
	/// If the operation fails, the result contains an error indicating the reason for failure.
	/// </returns>
	Task<ErrorOr<Created>> CreateItemAsync(Guid listId, ItemCreateRequest request, CancellationToken token = default);

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
	/// <returns>
	/// An <see cref="ErrorOr{T}"/> result containing <see cref="Deleted"/> if the operation succeeds.
	/// If the operation fails, the result contains an error indicating the reason for failure.
	/// </returns>
	Task<ErrorOr<Deleted>> DeleteListAsync(Guid listId, CancellationToken token = default);

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
	/// <returns>
	/// An <see cref="ErrorOr{T}"/> result containing <see cref="Deleted"/> if the operation succeeds.
	/// If the operation fails, the result contains an error indicating the reason for failure.
	/// </returns>
	Task<ErrorOr<Deleted>> DeleteItemAsync(Guid itemId, CancellationToken token = default);

	/// <summary>
	/// Retrieves all to-do lists asynchronously.
	/// </summary>
	/// <remarks>
	/// This method fetches all to-do lists from the repository and maps them to response
	/// objects. If an error occurs during the operation, an appropriate error is returned.
	/// </remarks>
	/// <param name="token">A cancellation token that can be used to cancel the operation.</param>
	/// <returns>
	/// An <see cref="ErrorOr{T}"/> containing either a collection of <see cref="ListResponse"/>
	/// objects representing the to-do lists, or an error indicating the failure reason.
	/// </returns>
	Task<ErrorOr<IEnumerable<ListResponse>>> GetAllListsAsync(CancellationToken token = default);

	/// <summary>
	/// Retrieves a to-do list by its unique identifier.
	/// </summary>
	/// <remarks>
	/// This method attempts to retrieve a to-do list from the repository, including its
	/// associated items. If the list is not found, an error is returned.
	/// </remarks>
	/// <param name="listId">The unique identifier of the to-do list to retrieve.</param>
	/// <param name="token">A cancellation token that can be used to cancel the operation.</param>
	/// <returns>
	/// An <see cref="ErrorOr{T}"/> containing either a <see cref="ListResponse"/> representing
	/// the to-do list or an error indicating the failure reason.
	/// </returns>
	Task<ErrorOr<ListResponse>> GetListAsync(Guid listId, CancellationToken token = default);

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
	/// <returns>
	/// An <see cref="ErrorOr{T}"/> result containing <see cref="Updated"/> if the operation succeeds.
	/// If the operation fails, the result contains an error indicating the reason for failure.
	/// </returns>
	Task<ErrorOr<Updated>> UpdateListAsync(Guid listId, ListUpdateRequest request, CancellationToken token = default);

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
	/// <returns>
	/// An <see cref="ErrorOr{T}"/> result containing <see cref="Updated"/> if the operation succeeds.
	/// If the operation fails, the result contains an error indicating the reason for failure.
	/// </returns>
	Task<ErrorOr<Updated>> UpdateItemAsync(Guid itemId, ItemUpdateRequest request, CancellationToken token = default);
}
