using Application.Interfaces.Infrastructure.Repositories;
using Domain.Entities.Finance;
using Domain.Extensions;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The account repository class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IAccountRepository"/> interface</item>
/// </list>
/// </remarks>
[SuppressMessage("Globalization", "CA1309",
	Justification = "Translation of the 'string.Equals' overload with a 'StringComparison' parameter is not supported.")]
internal sealed class AccountRepository : IdentityRepository<Account>, IAccountRepository
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
