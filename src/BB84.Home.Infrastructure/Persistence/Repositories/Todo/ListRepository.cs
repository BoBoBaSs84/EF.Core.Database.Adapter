using BB84.EntityFrameworkCore.Repositories;
using BB84.EntityFrameworkCore.Repositories.Abstractions;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;
using BB84.Home.Domain.Entities.Todo;

namespace BB84.Home.Infrastructure.Persistence.Repositories.Todo;

/// <summary>
/// The list repository class.
/// </summary>
/// <inheritdoc/>
internal sealed class ListRepository(IDbContext dbContext) : IdentityRepository<ListEntity>(dbContext), IListRepository
{ }
