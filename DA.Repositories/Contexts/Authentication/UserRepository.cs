using DA.Repositories.BaseTypes;
using DA.Repositories.Contexts.Authentication.Interfaces;
using DA.Models.Contexts.Authentication;
using Microsoft.EntityFrameworkCore;

namespace DA.Repositories.Contexts.Authentication;

/// <summary>
/// The user repository class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="GenericRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IUserRepository"/> interface</item>
/// </list>
/// </remarks>
internal sealed class UserRepository : GenericRepository<User>, IUserRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="UserRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public UserRepository(DbContext dbContext) : base(dbContext)
	{
	}
}
