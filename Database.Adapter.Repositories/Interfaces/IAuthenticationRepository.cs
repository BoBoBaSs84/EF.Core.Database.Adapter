using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.Interfaces;

public interface IAuthenticationRepository : IUnitOfWork<AuthenticationContext>
{
}
