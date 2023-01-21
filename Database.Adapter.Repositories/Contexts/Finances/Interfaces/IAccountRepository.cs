using Database.Adapter.Entities.Contexts.Finances;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.Contexts.Finances.Interfaces;

/// <summary>
/// The account repository interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface.</item>
/// </list>
/// </remarks>
public interface IAccountRepository : IGenericRepository<Account>
{
	/// <summary>
	/// Should get the account entity by the international bank account number.
	/// </summary>
	/// <param name="IBAN">The international bank account number.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns>A account entity.</returns>
	Account GetAccount(string IBAN, bool trackChanges = false);
	/// <summary>
	/// Should get a collection of account entities by the user identifier.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns>A collection of account entities.</returns>
	IEnumerable<Account> GetAccounts(int userId, bool trackChanges = false);
}
