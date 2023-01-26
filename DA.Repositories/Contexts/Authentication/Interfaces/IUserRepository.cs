using DA.Repositories.BaseTypes.Interfaces;
using DA.Models.Contexts.Authentication;

namespace DA.Repositories.Contexts.Authentication.Interfaces;

/// <summary>
/// The user repository interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface.</item>
/// </list>
/// </remarks>
public interface IUserRepository : IGenericRepository<User>
{
}
