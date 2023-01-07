using Database.Adapter.Entities.Authentication;
using Database.Adapter.Infrastructure.Extensions;
using Database.Adapter.Infrastructure.Factories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Infrastructure.Contexts;

/// <summary>
/// The authentication database context class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="IdentityDbContext{TUser, TRole, TKey}"/> class.
/// </remarks>

public sealed class AuthenticationContext : IdentityDbContext<CustomIdentityUser, CustomIdentityRole, int>
{
	/// <summary>
	/// The standard parameterless constructor.
	/// </summary>
	/// <remarks>
	/// Uses the <see cref="AuthenticationContextFactory"/> for options.
	/// </remarks>
	public AuthenticationContext() : base(AuthenticationContextFactory.DbContextOptions) => Database.EnsureCreated();

	/// <summary>
	/// The standard constructor.
	/// </summary>
	/// <param name="contextOptions">The database context options.</param>
	public AuthenticationContext(DbContextOptions<AuthenticationContext> contextOptions) : base(contextOptions)
	{
	}

	/// <inheritdoc/>
	[SuppressMessage("Style", "IDE0058", Justification = "Not needed here.")]
	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.HasDefaultSchema(SqlSchema.IDENTITY);

		builder.ApplyConfigurationsForContextEntities();

		base.OnModelCreating(builder);
	}
}
