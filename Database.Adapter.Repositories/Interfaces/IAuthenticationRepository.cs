using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Infrastructure.Interfaces;

namespace Database.Adapter.Repositories.Interfaces;

public interface IAuthenticationRepository : IUnitOfWork<AuthenticationContext>
{
}
