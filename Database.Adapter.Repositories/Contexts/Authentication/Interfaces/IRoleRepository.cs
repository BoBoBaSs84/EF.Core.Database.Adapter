using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.Contexts.Authentication.Interfaces;

/// <summary>
/// The role repository interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface.</item>
/// </list>
/// </remarks>
public interface IRoleRepository : IGenericRepository<Role>
{
}
