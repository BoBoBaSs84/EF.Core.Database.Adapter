using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Persistence;
using Domain.Entities.Identity;
using Infrastructure.Common;
using Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence;

/// <summary>
/// The application database context class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityDbContext{TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken}"/> class.
/// </remarks>
public sealed partial class RepositoryContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IRepositoryContext
{
	private readonly CustomSaveChangesInterceptor _changesInterceptor;
	private readonly ILoggerWrapper<RepositoryContext> _logger;

	private static readonly Action<ILogger, Exception?> logException =
	LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	/// <summary>
	/// Initializes a new instance of the <see cref="RepositoryContext"/> class.
	/// </summary>
	/// <param name="dbContextOptions">The database context options.</param>
	/// <param name="changesInterceptor">The auditable entity save changes interceptor.</param>
	/// <param name="logger">The logger service.</param>
	public RepositoryContext(
		DbContextOptions<RepositoryContext> dbContextOptions,
		CustomSaveChangesInterceptor changesInterceptor,
		ILoggerWrapper<RepositoryContext> logger
		) : base(dbContextOptions)
	{
		_changesInterceptor = changesInterceptor;
		_logger = logger;

		ChangeTracker.LazyLoadingEnabled = false;
		ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
	}

	/// <inheritdoc/>
	[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.HasDefaultSchema(SqlSchema.PRIVATE)
			.ApplyConfigurationsFromAssembly(typeof(IInfrastructureAssemblyMarker).Assembly);
	}

	/// <inheritdoc/>
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.AddInterceptors(_changesInterceptor);

	/// <inheritdoc/>
	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		int i = 0;
		try
		{
			i = await base.SaveChangesAsync(cancellationToken);
		}
		// TODO: What todo else then?
		catch (DbUpdateConcurrencyException ex)
		{
			_logger.Log(logException, ex);
		}
		// TODO: What todo else then?
		catch (DbUpdateException ex)
		{
			_logger.Log(logException, ex);
		}
		return i;
	}
}
