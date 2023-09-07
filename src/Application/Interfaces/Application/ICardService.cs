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
	/// Creates a bank card for the application user and bank account.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="request">The bank card create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> Create(Guid userId, Guid accountId, CardCreateRequest request, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes an existing bank card for the application user by the bank card identifier.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> Delete(Guid userId, Guid cardId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a bank card for the application user by the bank card identifier.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<CardResponse>> Get(Guid userId, Guid cardId, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a bank card for the application user by the payment card number.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="pan">The payment card number of the bank card.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<CardResponse>> Get(Guid userId, string pan, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a collection of bank cards for for the application user.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IEnumerable<CardResponse>>> Get(Guid userId, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates an existing bank card for the application user.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="request">The bank card update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> Update(Guid userId, CardUpdateRequest request, CancellationToken cancellationToken = default);
}
