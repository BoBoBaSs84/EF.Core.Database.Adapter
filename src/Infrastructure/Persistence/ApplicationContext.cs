using Domain.Entities.Identity;
using Infrastructure.Common;
using Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Schema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence;

/// <summary>
/// The application database context class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityDbContext{TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken}"/> class.
/// </remarks>
public sealed partial class ApplicationContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
	private readonly CustomSaveChangesInterceptor _changesInterceptor;

	/// <summary>
	/// Initializes a new instance of the <see cref="ApplicationContext"/> class.
	/// </summary>
	/// <param name="dbContextOptions">The database context options.</param>
	/// <param name="changesInterceptor">The auditable entity save changes interceptor.</param>
	public ApplicationContext(DbContextOptions<ApplicationContext> dbContextOptions, CustomSaveChangesInterceptor changesInterceptor)
		: base(dbContextOptions) => _changesInterceptor = changesInterceptor;

	/// <inheritdoc/>
	[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.HasDefaultSchema(Schema.PRIVATE)
			.ApplyConfigurationsFromAssembly(typeof(IInfrastructureAssemblyMarker).Assembly);
	}

	/// <inheritdoc/>
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
		optionsBuilder.AddInterceptors(_changesInterceptor);

	/// <inheritdoc/>
	public override int SaveChanges()
	{
		// TODO: other events?
		return base.SaveChanges();
	}

	/// <inheritdoc/>
	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		// TODO: other events?
		return await base.SaveChangesAsync(cancellationToken);
	}
}
