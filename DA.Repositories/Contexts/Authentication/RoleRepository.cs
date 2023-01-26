using DA.Repositories.BaseTypes;
using DA.Repositories.Contexts.Authentication.Interfaces;
using DA.Models.Contexts.Authentication;
using Microsoft.EntityFrameworkCore;

namespace DA.Repositories.Contexts.Authentication;

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
