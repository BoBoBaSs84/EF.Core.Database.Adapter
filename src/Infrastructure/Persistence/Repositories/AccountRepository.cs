using Application.Interfaces.Infrastructure.Persistence.Repositories;

using Domain.Models.Finance;

using Infrastructure.Persistence.Repositories.Base;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The account repository class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityRepository{TEntity}"/> class
/// and implements the <see cref="IAccountRepository"/> interface.
/// </remarks>
internal sealed class AccountRepository : IdentityRepository<AccountModel>, IAccountRepository
{
	/// <summary>
	/// Initializes a new instance of the account repository class.
	/// </summary>
	/// <inheritdoc/>
	public AccountRepository(DbContext dbContext) : base(dbContext)
	{ }
}
