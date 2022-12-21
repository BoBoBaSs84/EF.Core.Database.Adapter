using Database.Adapter.Infrastructure.Factories;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Infrastructure.Contexts;

/// <summary>
/// The master database context class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="DbContext"/> class.
/// </remarks>
public sealed partial class MasterContext : DbContext
{
	/// <summary>
	/// The standard parameterless constructor.
	/// </summary>
	/// <remarks>
	/// Uses the <see cref="MasterContextFactory"/> for options.
	/// </remarks>
	public MasterContext() : base()
	{
	}

	/// <summary>
	/// The standard constructor.
	/// </summary>
	/// <param name="contextOptions">The database context options.</param>
	public MasterContext(DbContextOptions<MasterContext> contextOptions) : base(contextOptions)
	{
	}
}
