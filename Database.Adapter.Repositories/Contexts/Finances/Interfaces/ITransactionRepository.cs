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
	/// <returns>A collection of transaction entities.</returns>
	IEnumerable<Transaction> GetCardTransaction(int userId, int cardId, bool trackChanges = false);
	/// <summary>
	/// Should get a collection of transaction entities by the user identifier and the card number.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="cardNumber">The number of the card.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns>A collection of transaction entities.</returns>
	IEnumerable<Transaction> GetCardTransaction(int userId, string cardNumber, bool trackChanges = false);
	/// <summary>
	/// Should get a collection of transaction entities by the user identifier and the account identifier.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="accountId">The identifier of the account.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns>A collection of transaction entities.</returns>
	IEnumerable<Transaction> GetAccountTransaction(int userId, int accountId, bool trackChanges = false);
	/// <summary>
	/// Should get a collection of transaction entities by the user identifier and the account number.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="accountNumber">The number of the account.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns>A collection of transaction entities.</returns>
	IEnumerable<Transaction> GetAccountTransaction(int userId, string accountNumber, bool trackChanges = false);
}
