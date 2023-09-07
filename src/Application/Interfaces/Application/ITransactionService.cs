using Application.Contracts.Responses.Finance;
using Application.Features.Requests;
using Application.Features.Responses;

using Domain.Errors;

namespace Application.Interfaces.Application;

/// <summary>
/// The transaction service interface.
/// </summary>
public interface ITransactionService
{
	/// <summary>
	/// Returns a bank transaction by its identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank transaction.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<TransactionResponse>> GetById(Guid id, bool trackChanges = false, CancellationToken cancellationToken = default);

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
	/// Returns multiple transaction entries as a paged list for a bank account filtered by the transaction query parameters.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="parameters">The transaction query parameters.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IPagedList<TransactionResponse>>> GetByAccountId(Guid accountId, TransactionParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default);
}
