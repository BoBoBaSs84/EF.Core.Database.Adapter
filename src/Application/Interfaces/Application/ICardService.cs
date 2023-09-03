using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;

using Domain.Errors;
using Domain.Results;

namespace Application.Interfaces.Application;

/// <summary>
/// The card service Guiderface.
/// </summary>
public interface ICardService
{
	/// <summary>
	/// Creates a card for the given <paramref name="userId"/> and <paramref name="accountId"/>.
	/// </summary>
	/// <param name="userId">The user identifier.</param>
	/// <param name="accountId">The account identifier.</param>
	/// <param name="createRequest">The card create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> Create(Guid userId, Guid accountId, CardCreateRequest createRequest, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes a card by the <paramref name="cardId"/> for the given <paramref name="userId"/>.
	/// </summary>
	/// <param name="userId">The user identifier.</param>
	/// <param name="cardId">The card identifier.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> Delete(Guid userId, Guid cardId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a card by the <paramref name="cardId"/> for the given <paramref name="userId"/>.
	/// </summary>
	/// <param name="userId">The user identifier.</param>
	/// <param name="cardId">The card identifier.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<CardResponse>> Get(Guid userId, Guid cardId, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a card by the <paramref name="pan"/> (payment card number) for the given <paramref name="userId"/>.
	/// </summary>
	/// <param name="userId">The user identifier.</param>
	/// <param name="pan"></param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<CardResponse>> Get(Guid userId, string pan, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a collection of cards for the given <paramref name="userId"/>.
	/// </summary>
	/// <param name="userId">The user identifier.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IEnumerable<CardResponse>>> Get(Guid userId, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates an existing card for the given <paramref name="userId"/>.
	/// </summary>
	/// <param name="userId">The user identifier.</param>
	/// <param name="updateRequest">The card update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> Update(Guid userId, CardUpdateRequest updateRequest, CancellationToken cancellationToken = default);
}
