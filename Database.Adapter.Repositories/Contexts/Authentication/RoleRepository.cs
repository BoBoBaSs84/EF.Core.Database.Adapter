using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Contexts.Authentication.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Repositories.Contexts.Authentication;

/// <summary>
/// The role repository class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="GenericRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IRoleRepository"/> interface</item>
/// </list>
/// </remarks>
internal sealed class RoleRepository : GenericRepository<Role>, IRoleRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="RoleRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public RoleRepository(DbContext dbContext) : base(dbContext)
	{
	}
}
