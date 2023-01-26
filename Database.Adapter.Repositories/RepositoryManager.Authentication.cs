using Database.Adapter.Repositories.Contexts.Authentication;
using Database.Adapter.Repositories.Contexts.Authentication.Interfaces;

namespace Database.Adapter.Repositories;

public sealed partial class RepositoryManager
{
	private readonly Lazy<IUserRepository> lazyUserRepository = default!;

	/// <inheritdoc/>
	public IUserRepository UserRepository =>
		lazyUserRepository.Value ?? new Lazy<IUserRepository>(() => new UserRepository(DbContext)).Value;
}
