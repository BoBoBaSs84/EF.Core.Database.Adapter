using Database.Adapter.Entities.Contexts.Finances;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.Contexts.Finances.Interfaces;

/// <summary>
/// The transaction repository interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface.</item>
/// </list>
/// </remarks>
public interface ITransactionRepository : IGenericRepository<Transaction>
{
	/// <summary>
	/// Should get a collection of transaction entities by the user identifier and the card identifier.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="cardId">The identifier of the card.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A collection of transaction entities.</returns>
	Task<IEnumerable<Transaction>> GetCardTransactionAsync(int userId, int cardId, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should get a collection of transaction entities by the user identifier and the primary account number.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="pan">The number of the card aka. <b>p</b>rimary <b>a</b>ccount <b>n</b>umber.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A collection of transaction entities.</returns>
	Task<IEnumerable<Transaction>> GetCardTransactionAsync(int userId, string pan, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should get a collection of transaction entities by the user identifier and the account identifier.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="accountId">The identifier of the account.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A collection of transaction entities.</returns>
	Task<IEnumerable<Transaction>> GetAccountTransactionAsync(int userId, int accountId, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should get a collection of transaction entities by the user identifier and the international bank account number.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="iban">The international bank account number.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A collection of transaction entities.</returns>
	Task<IEnumerable<Transaction>> GetAccountTransactionAsync(int userId, string iban, bool trackChanges = false, CancellationToken cancellationToken = default);
}
