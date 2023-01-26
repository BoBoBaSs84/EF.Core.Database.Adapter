using DA.Repositories.Contexts.Authentication.Interfaces;

namespace Database.Adapter.Repositories.Interfaces;

public partial interface IRepositoryManager
{
	/// <summary>
	/// The <see cref="UserRepository"/> interface.
	/// </summary>
	IUserRepository UserRepository { get; }
}
