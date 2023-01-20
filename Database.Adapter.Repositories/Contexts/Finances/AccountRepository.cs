using Database.Adapter.Entities.Contexts.Finances;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Contexts.Finances.Interfaces;
using Database.Adapter.Entities.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Repositories.Contexts.Finances;

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

	public Account GetAccount(string IBAN, bool trackChanges = false) =>
		GetByCondition(
			expression: x => x.IBAN.Equals(IBAN.RemoveWhitespace()),
			trackChanges: trackChanges
			);
	public IEnumerable<Account> GetAccounts(int userId, bool trackChanges = false) =>
		GetManyByCondition(
			expression: x => x.AccountUsers.Select(x => x.UserId).Contains(userId),
			trackChanges: trackChanges
			);
}
