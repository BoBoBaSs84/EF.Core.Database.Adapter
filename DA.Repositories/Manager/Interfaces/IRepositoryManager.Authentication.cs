using DA.Repositories.Contexts.Authentication.Interfaces;

namespace DA.Repositories.Manager.Interfaces;

public partial interface IRepositoryManager
{
	/// <summary>
	/// The <see cref="UserRepository"/> interface.
	/// </summary>
	IUserRepository UserRepository { get; }
}
