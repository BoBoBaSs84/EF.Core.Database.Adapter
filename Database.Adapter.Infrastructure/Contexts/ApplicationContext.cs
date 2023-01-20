using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Infrastructure.Extensions;
using Database.Adapter.Infrastructure.Factories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Infrastructure.Contexts;

/// <summary>
/// The application database context class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="IdentityDbContext{TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken}"/> class.
/// </remarks>
public sealed partial class ApplicationContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ApplicationContext"/> class.
	/// </summary>
	public ApplicationContext() : base(ApplicationContextFactory.DbContextOptions)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ApplicationContext"/> class.
	/// </summary>
	/// <param name="dbContextOptions"></param>
	public ApplicationContext(DbContextOptions<ApplicationContext> dbContextOptions) : base(dbContextOptions)
	{
	}

	/// <inheritdoc/>
	[SuppressMessage("Style", "IDE0058", Justification = "Not needed here.")]
	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.HasDefaultSchema(SqlSchema.PRIVATE);

		builder.ApplyConfigurationsForContextEntities();
	}
}
