using Database.Adapter.Entities.Contexts.Finances;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Contexts.Finances.Interfaces;
using Microsoft.EntityFrameworkCore;

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
internal sealed class AccountRepository : GenericRepository<Account>, IAccountRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="AccountRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public AccountRepository(DbContext dbContext) : base(dbContext)
	{
	}
}
