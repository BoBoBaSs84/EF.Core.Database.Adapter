using Application.Interfaces.Infrastructure.Persistence;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;

using BB84.EntityFrameworkCore.Repositories;

using Domain.Entities.Todo;

namespace Infrastructure.Persistence.Repositories.Todo;

/// <summary>
/// The list repository class.
/// </summary>
/// <param name="repositoryContext">The repository context to use.</param>
internal sealed class ListRepository(IRepositoryContext repositoryContext) : IdentityRepository<ListEntity>(repositoryContext), IListRepository
{ }
