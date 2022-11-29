using Database.Adapter.Infrastructure.Extensions;
using Database.Adapter.Infrastructure.Factory;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Infrastructure.Contexts;

/// <summary>
/// The master data context class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="DbContext"/> class.
/// </remarks>
public sealed partial class MasterDataContext : DbContext
{
	/// <summary>
	/// The standard parameterless constructor.
	/// </summary>
	/// <remarks>
	/// Uses the <see cref="MasterDataContextFactory"/> for options.
	/// </remarks>
	public MasterDataContext() : base(MasterDataContextFactory.DbContextOptions)
	{		
	}

	/// <summary>
	/// The standard constructor.
	/// </summary>
	/// <param name="contextOptions">The database context options.</param>
	public MasterDataContext(DbContextOptions<MasterDataContext> contextOptions) : base(contextOptions)
	{
	}

	/// <inheritdoc/>
	[SuppressMessage("Style", "IDE0058", Justification = "Not needed here.")]
	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.HasDefaultSchema(SqlSchema.PRIVATE);
		builder.ApplyConfigurationsForContextEntities();

		base.OnModelCreating(builder);
	}
}
