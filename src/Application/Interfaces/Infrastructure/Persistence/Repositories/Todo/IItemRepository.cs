using BB84.EntityFrameworkCore.Repositories.Abstractions;
using BB84.Home.Domain.Entities.Todo;

namespace BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;

/// <summary>
/// The interface for the todo item repository.
/// </summary>
/// <inheritdoc/>
public interface IItemRepository : IIdentityRepository<ItemEntity>
{ }
