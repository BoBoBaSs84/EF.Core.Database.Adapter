using Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;

using BB84.EntityFrameworkCore.Repositories;
using BB84.EntityFrameworkCore.Repositories.Abstractions;

using Domain.Entities.Todo;

namespace Infrastructure.Persistence.Repositories.Todo;

/// <summary>
/// The item repository class.
/// </summary>
/// <inheritdoc/>
internal sealed class ItemRepository(IDbContext dbContext) : IdentityRepository<ItemEntity>(dbContext), IItemRepository
{ }
