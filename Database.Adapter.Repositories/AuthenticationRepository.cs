using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Contexts.Authentication;
using Database.Adapter.Repositories.Contexts.Authentication.Interfaces;
using Database.Adapter.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

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
	private readonly Lazy<IUserRepository> lazyUserRepository;

	/// <summary>
	/// Initializes a new instance of the <see cref="MasterDataRepository"/> class.
	/// </summary>
	public AuthenticationRepository(UserManager<User> userManager = default!)
	{
		lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository(Context));
	}

	/// <inheritdoc/>
	public IUserRepository UserRepository => lazyUserRepository.Value;
}
