using BB84.EntityFrameworkCore.Repositories;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;
using BB84.Home.Domain.Entities.Todo;

namespace BB84.Home.Infrastructure.Persistence.Repositories.Todo;

/// <summary>
/// Represents the repository for the <see cref="ListEntity"/> entity.
/// </summary>
internal sealed class ListRepository(IRepositoryContext repositoryContext)
	: IdentityRepository<ListEntity>(repositoryContext), IListRepository
{ }
