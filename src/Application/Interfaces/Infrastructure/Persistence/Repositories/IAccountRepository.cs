using BB84.EntityFrameworkCore.Repositories.Abstractions;
using BB84.Home.Domain.Entities.Finance;

namespace BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;

/// <summary>
/// The account repository interface.
/// </summary>
/// <inheritdoc/>
public interface IAccountRepository : IIdentityRepository<AccountEntity>
{ }
