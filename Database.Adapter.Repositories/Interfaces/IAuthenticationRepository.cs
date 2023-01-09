using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.Interfaces;

/// <summary>
/// The authentication repository interface.
/// </summary>
public interface IAuthenticationRepository : IUnitOfWork<AuthenticationContext>
{
}
