using DA.Infrastructure.Data;
using DA.Repositories.BaseTypes;
using DA.Repositories.Manager.Interfaces;

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
	/// Initializes a new instance of the <see cref="RepositoryManager"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public RepositoryManager(ApplicationContext dbContext) : base(dbContext)
	{
		InitializeAuthentication();
		InitializeMasterData();
		InitializeTimekeeping();
		InitializeFinances();
	}
}
