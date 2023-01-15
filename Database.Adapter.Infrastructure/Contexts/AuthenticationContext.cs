using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Infrastructure.Extensions;
using Database.Adapter.Infrastructure.Factories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Infrastructure.Contexts;

/// <summary>
/// The authentication database context class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="IdentityDbContext{TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken}"/> class.
/// </remarks>

public sealed class AuthenticationContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
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
	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.ApplyConfigurationsForContextEntities();
	}
}
