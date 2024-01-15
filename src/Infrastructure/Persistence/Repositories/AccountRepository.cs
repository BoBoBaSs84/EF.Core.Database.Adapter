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
/// <remarks>
/// Initializes a new instance of the account repository class.
/// </remarks>
/// <inheritdoc/>
internal sealed class AccountRepository(DbContext dbContext) : IdentityRepository<AccountModel>(dbContext), IAccountRepository
{
}
