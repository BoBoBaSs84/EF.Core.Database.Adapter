using DA.Repositories.BaseTypes;
using Database.Adapter.Infrastructure.Contexts;
using DA.Repositories.Manager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DA.Repositories.Manager;

/// <summary>
/// The master repository class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="UnitOfWork{TContext}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IRepositoryManager"/> interface</item>
/// </list>
/// </remarks>
internal sealed partial class RepositoryManager : UnitOfWork<ApplicationContext>, IRepositoryManager
{
	/// <summary>
	/// The <see cref="DbContext"/> property.
	/// </summary>
	public DbContext DbContext { get; private set; } = default!;

	/// <summary>
	/// Initializes a new instance of the <see cref="RepositoryManager"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public RepositoryManager(DbContext? dbContext = null)
	{
		DbContext = (dbContext is null) ? Context : dbContext;
		InitializeAuthentication();
		InitializeMasterData();
		InitializeTimekeeping();
		InitializeFinances();
	}
}
