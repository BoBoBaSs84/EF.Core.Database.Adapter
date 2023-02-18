using DA.Models.Contexts.Finances;
using DA.Repositories.BaseTypes.Interfaces;

namespace DA.Repositories.Contexts.Finances.Interfaces;

/// <summary>
/// The account repository interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IIdentityRepository{TIdentityEntity}"/> interface.</item>
/// </list>
/// </remarks>
public interface IAccountRepository : IIdentityRepository<Account>
{
	/// <summary>
	/// Should get a account entity by the international bank account number.
	/// </summary>
	/// <param name="iban">The international bank account number.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A account entity.</returns>
	Task<Account> GetAccountAsync(string iban, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should get a collection of account entities by the user identifier.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A collection of account entities.</returns>
	Task<IEnumerable<Account>> GetAccountsAsync(int userId, bool trackChanges = false, CancellationToken cancellationToken = default);
}
