using DA.Models.Contexts.Finances;
using DA.Models.Extensions;
using DA.Repositories.BaseTypes;
using DA.Repositories.Contexts.Finances.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DA.Repositories.Contexts.Finances;

/// <summary>
/// The account repository class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="GenericRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IAccountRepository"/> interface</item>
/// </list>
/// </remarks>
[SuppressMessage("Globalization", "CA1309", Justification = "Translation of the 'string.Equals' overload with a 'StringComparison' parameter is not supported.")]
internal sealed class AccountRepository : GenericRepository<Account>, IAccountRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="AccountRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public AccountRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public async Task<Account> GetAccountAsync(string iban,
		bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetByConditionAsync(
			expression: x => x.IBAN.Equals(iban.RemoveWhitespace()),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);

	public async Task<IEnumerable<Account>> GetAccountsAsync(int userId,
		bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x => x.AccountUsers.Select(x => x.UserId).Contains(userId),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);
}
