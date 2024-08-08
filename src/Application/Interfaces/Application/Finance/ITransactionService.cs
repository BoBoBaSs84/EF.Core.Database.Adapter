using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Features.Requests;
using Application.Features.Responses;

using Domain.Errors;
using Domain.Results;

namespace Application.Interfaces.Application.Finance;

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
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> CreateByAccountId(Guid accountId, TransactionCreateRequest request, CancellationToken token = default);

	/// <summary>
	/// Creates a new transaction for the bank card.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="request">The transaction create request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> CreateByCardId(Guid cardId, TransactionCreateRequest request, CancellationToken token = default);

	/// <summary>
	/// Deletes an existing transaction for the bank account.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="id">The identifier of the transaction.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> DeleteByAccountId(Guid accountId, Guid id, CancellationToken token = default);

	/// <summary>
	/// Deletes an existing transaction for the bank card.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="id">The identifier of the transaction.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> DeleteByCardId(Guid cardId, Guid id, CancellationToken token = default);

	/// <summary>
	/// Returns an existing transaction for the bank account.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="id">The identifier of the bank transaction.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<TransactionResponse>> GetByAccountId(Guid accountId, Guid id, CancellationToken token = default);

	/// <summary>
	/// Returns an existing transaction for the bank card.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="id">The identifier of the bank transaction.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<TransactionResponse>> GetByCardId(Guid cardId, Guid id, CancellationToken token = default);

	/// <summary>
	/// Returns multiple transaction entries as a paged list for a bank account filtered by the transaction query parameters.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="parameters">The transaction query parameters.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IPagedList<TransactionResponse>>> GetPagedByAccountId(Guid accountId, TransactionParameters parameters, CancellationToken token = default);

	/// <summary>
	/// Returns multiple transaction entries as a paged list for a bank card filtered by the transaction query parameters.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="parameters">The transaction query parameters.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IPagedList<TransactionResponse>>> GetPagedByCardId(Guid cardId, TransactionParameters parameters, CancellationToken token = default);

	/// <summary>
	/// Updates an existing transaction for the bank account.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="id">The identifier of the bank transaction to update.</param>
	/// <param name="request">The transaction update request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> UpdateByAccountId(Guid accountId, Guid id, TransactionUpdateRequest request, CancellationToken token = default);

	/// <summary>
	/// Updates an existing transaction for the card account.
	/// </summary>
	/// <param name="cardId">The identifier of the bank account.</param>
	/// <param name="id">The identifier of the bank transaction to update.</param>
	/// <param name="request">The transaction update request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> UpdateByCardId(Guid cardId, Guid id, TransactionUpdateRequest request, CancellationToken token = default);
}
