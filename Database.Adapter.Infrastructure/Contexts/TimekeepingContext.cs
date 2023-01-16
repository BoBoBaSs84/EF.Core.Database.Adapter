using Database.Adapter.Infrastructure.Configurations.Timekeeping;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Infrastructure.Contexts;

/// <summary>
/// The time keeping database context class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="DbContext"/> class.
/// </remarks>
public sealed partial class TimekeepingContext : DbContext
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TimekeepingContext"/> class.
	/// </summary>
	public TimekeepingContext() : base() => Database.EnsureCreated();

	/// <summary>
	/// Initializes a new instance of the <see cref="TimekeepingContext"/> class.
	/// </summary>
	/// <param name="contextOptions">The database context options.</param>
	public TimekeepingContext(DbContextOptions<TimekeepingContext> contextOptions) : base(contextOptions)
	{
	}

	/// <inheritdoc/>
	[SuppressMessage("Style", "IDE0058", Justification = "Not needed here.")]
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.HasDefaultSchema(SqlSchema.PRIVATE);

		modelBuilder.ApplyConfigurationsForContextEntities();
	}
}
