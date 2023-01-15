using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.Contexts.Authentication.Interfaces;

/// <summary>
/// The user repository interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface. The entity is <see cref="User"/>.</item>
/// </list>
/// </remarks>
public interface IUserRepository : IGenericRepository<User>
{
	/// <summary>
	/// Should get the user entity by the users user name.
	/// </summary>
	/// <param name="userName">The user name of the user.</param>
	/// <param name="trackchanges">Should the fetched entry be tracked?</param>
	/// <returns>A user entity.</returns>
	User GetByUserName(string userName, bool trackchanges = false);
	/// <summary>
	/// Should get the user entity by the users email adress.
	/// </summary>
	/// <param name="email">The users email adress.</param>
	/// <param name="trackchanges">Should the fetched entry be tracked?</param>
	/// <returns>A user entity.</returns>
	User GetByEmail(string email, bool trackchanges = false);
}
