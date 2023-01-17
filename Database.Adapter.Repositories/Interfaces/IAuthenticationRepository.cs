using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.Interfaces;

/// <summary>
/// The authentication repository interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IUnitOfWork{TContext}"/> interface</item>
/// </list>
/// </remarks>
public interface IAuthenticationRepository : IUnitOfWork<ApplicationContext>
{
}
