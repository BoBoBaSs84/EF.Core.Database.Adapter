using Application.Interfaces.Infrastructure.Persistence;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;

using BB84.EntityFrameworkCore.Repositories;

using Domain.Entities.Todo;

namespace Infrastructure.Persistence.Repositories.Todo;

/// <summary>
/// The item repository class.
/// </summary>
/// <param name="repositoryContext">The repository context to use.</param>
internal sealed class ItemRepository(IRepositoryContext repositoryContext) : IdentityRepository<ItemEntity>(repositoryContext), IItemRepository
{ }
