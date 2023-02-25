using DA.Domain.Models.Identity;
using DA.Repositories.BaseTypes.Interfaces;

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
