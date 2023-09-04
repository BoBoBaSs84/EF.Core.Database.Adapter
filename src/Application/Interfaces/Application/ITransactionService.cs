using Application.Contracts.Responses.Finance;

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
}
