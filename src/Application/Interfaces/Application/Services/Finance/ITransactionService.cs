﻿using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Contracts.Responses.Finance;
using BB84.Home.Application.Features.Requests;
using BB84.Home.Application.Features.Responses;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

namespace BB84.Home.Application.Interfaces.Application.Services.Finance;

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
	/// <param name="id">The identifier of the bank account.</param>
	/// <param name="parameters">The transaction query parameters.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IPagedList<TransactionResponse>>> GetPagedByAccountId(Guid id, TransactionParameters parameters, CancellationToken token = default);

	/// <summary>
	/// Returns multiple transaction entries as a paged list for a bank card filtered by the transaction query parameters.
	/// </summary>
	/// <param name="id">The identifier of the bank card.</param>
	/// <param name="parameters">The transaction query parameters.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IPagedList<TransactionResponse>>> GetPagedByCardId(Guid id, TransactionParameters parameters, CancellationToken token = default);

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
