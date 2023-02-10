using DA.Infrastructure.Extensions;
using DA.Infrastructure.Factories;
using DA.Models.Contexts.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static DA.Models.Constants.Sql;

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
	/// <param name="dbContextOptions">The database context options.</param>
	public ApplicationContext(DbContextOptions<ApplicationContext> dbContextOptions) : base(dbContextOptions)
	{
	}

	/// <inheritdoc/>	
	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.HasDefaultSchema(Schema.PRIVATE)
			.ApplyConfigurationsForContextEntities();
	}
}
