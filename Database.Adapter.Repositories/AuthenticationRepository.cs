using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Interfaces;

namespace Database.Adapter.Repositories;

public sealed class AuthenticationRepository : UnitOfWork<AuthenticationContext>, IAuthenticationRepository
{
}
