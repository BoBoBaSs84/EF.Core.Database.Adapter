using Application.Interfaces.Infrastructure.Persistence.Repositories;

using BB84.EntityFrameworkCore.Repositories;

using Domain.Models.Finance;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The account repository class.
/// </summary>
/// <inheritdoc/>
internal sealed class AccountRepository(DbContext dbContext) : IdentityRepository<AccountModel>(dbContext), IAccountRepository
{ }
