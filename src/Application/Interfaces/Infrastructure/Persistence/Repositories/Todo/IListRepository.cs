using BB84.EntityFrameworkCore.Repositories.Abstractions;

using Domain.Models.Todo;

namespace Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;

/// <summary>
/// Theinterface for the todo list repository.
/// </summary>
public interface IListRepository : IIdentityRepository<List>
{ }
