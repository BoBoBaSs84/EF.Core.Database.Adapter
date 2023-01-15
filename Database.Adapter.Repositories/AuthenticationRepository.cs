using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Interfaces;

namespace Database.Adapter.Repositories;

/// <summary>
/// The authentication repository class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="UnitOfWork{TContext}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IAuthenticationRepository"/> interface</item>
/// </list>
/// </remarks>
public sealed class AuthenticationRepository : UnitOfWork<AuthenticationContext>, IAuthenticationRepository
{
}
