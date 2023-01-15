using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes.Interfaces;
using Database.Adapter.Repositories.Contexts.Authentication.Interfaces;

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
public interface IAuthenticationRepository : IUnitOfWork<AuthenticationContext>
{
	/// <summary>The user repository interface.</summary>
	IUserRepository UserRepository { get; }
}
