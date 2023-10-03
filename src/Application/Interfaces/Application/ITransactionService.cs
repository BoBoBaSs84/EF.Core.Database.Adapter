using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Features.Requests;
using Application.Features.Responses;

using Domain.Errors;
using Domain.Results;

namespace Application.Interfaces.Application;

/// <summary>
/// The transaction service interface.
/// </summary>
public interface ITransactionService
{
	/// <summary>
	/// Creates a new transaction for the bank account.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="request">The transaction create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> CreateByAccountId(Guid accountId, TransactionCreateRequest request, CancellationToken cancellationToken = default);

	/// <summary>
	/// Creates a new transaction for the bank card.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="request">The transaction create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> CreateByCardId(Guid cardId, TransactionCreateRequest request, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes an existing transaction for the bank account.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="transactionId">The identifier of the transaction.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> DeleteByAccountId(Guid accountId, Guid transactionId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes an existing transaction for the bank card.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="transactionId">The identifier of the transaction.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> DeleteByCardId(Guid cardId, Guid transactionId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns an existing transaction for the bank account.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="transactionId">The identifier of the bank transaction.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<TransactionResponse>> GetByAccountId(Guid accountId, Guid transactionId, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns an existing transaction for the bank card.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="transactionId">The identifier of the bank transaction.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<TransactionResponse>> GetByCardId(Guid cardId, Guid transactionId, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns multiple transaction entries as a paged list for a bank account filtered by the transaction query parameters.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="parameters">The transaction query parameters.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IPagedList<TransactionResponse>>> GetByAccountId(Guid accountId, TransactionParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns multiple transaction entries as a paged list for a bank card filtered by the transaction query parameters.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="parameters">The transaction query parameters.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IPagedList<TransactionResponse>>> GetByCardId(Guid cardId, TransactionParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates an existing transaction for the bank account.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="request">The transaction update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> UpdateByAccountId(Guid accountId, TransactionUpdateRequest request, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates an existing transaction for the card account.
	/// </summary>
	/// <param name="cardId">The identifier of the bank account.</param>
	/// <param name="request">The transaction update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> UpdateByCardId(Guid cardId, TransactionUpdateRequest request, CancellationToken cancellationToken = default);
}
