using DA.Repositories.Contexts.Authentication.Interfaces;
using DA.Repositories.Contexts.Authentication;

namespace Database.Adapter.Repositories;

public sealed partial class RepositoryManager
{
	private Lazy<IRoleRepository> lazyRoleRepository = default!;
	private Lazy<IUserRepository> lazyUserRepository = default!;

	/// <inheritdoc/>
	public IRoleRepository RoleRepository => lazyRoleRepository.Value;
	/// <inheritdoc/>
	public IUserRepository UserRepository => lazyUserRepository.Value;

	private void InitializeAuthentication()
	{
		lazyRoleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(DbContext));
		lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository(DbContext));
	}
}
