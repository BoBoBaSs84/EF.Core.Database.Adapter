using BB84.EntityFrameworkCore.Repositories.Abstractions;
using BB84.Home.Domain.Entities.Todo;

namespace BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;

/// <summary>
/// Theinterface for the todo list repository.
/// </summary>
/// <inheritdoc/>
public interface IListRepository : IIdentityRepository<ListEntity>
{ }
