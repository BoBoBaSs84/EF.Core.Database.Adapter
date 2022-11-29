using Database.Adapter.Infrastructure;
using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.Data.Interfaces;

namespace Database.Adapter.Repositories.Data;

public sealed class AuthenticationRepository : UnitOfWork<AuthenticationContext>, IAuthenticationRepository
{
}
