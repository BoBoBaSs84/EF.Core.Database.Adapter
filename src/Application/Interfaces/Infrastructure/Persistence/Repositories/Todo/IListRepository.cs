using BB84.EntityFrameworkCore.Repositories.Abstractions;

using Domain.Entities.Todo;

namespace Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;

/// <summary>
/// Theinterface for the todo list repository.
/// </summary>
/// <inheritdoc/>
public interface IListRepository : IIdentityRepository<ListEntity>
{ }
