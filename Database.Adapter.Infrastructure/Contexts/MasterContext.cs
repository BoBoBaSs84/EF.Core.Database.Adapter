using Database.Adapter.Infrastructure.Extensions;
using Database.Adapter.Infrastructure.Factories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

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
	/// Initializes a new instance of the <see cref="MasterContext"/> class.
	/// </summary>
	public MasterContext() : base(MasterContextFactory.DbContextOptions)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="MasterContext"/> class.
	/// </summary>
	/// <param name="dbContextOptions">The database context options.</param>
	public MasterContext(DbContextOptions<MasterContext> dbContextOptions) : base(dbContextOptions)
	{
	}

	/// <inheritdoc/>
	[SuppressMessage("Style", "IDE0058", Justification = "Not needed here.")]
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsForContextEntities();
	}
}
