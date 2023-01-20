using Database.Adapter.Repositories.Contexts.Authentication.Interfaces;

namespace Database.Adapter.Repositories;

public sealed partial class RepositoryManager
{
	private readonly Lazy<IUserRepository> lazyUserRepository;

	/// <inheritdoc/>
	public IUserRepository UserRepository => lazyUserRepository.Value;
}
