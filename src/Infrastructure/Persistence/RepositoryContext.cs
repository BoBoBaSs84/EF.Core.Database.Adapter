using Application.Interfaces.Infrastructure.Services;

using BB84.EntityFrameworkCore.Repositories.SqlServer.Interceptors;

using Domain.Models.Identity;

using Infrastructure.Common;
using Infrastructure.Interfaces.Persistence;
using Infrastructure.Persistence.Interceptors;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

/// <summary>
/// The application repository context class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityDbContext"/> class.
/// </remarks>
public sealed partial class RepositoryContext : IdentityDbContext<UserModel, RoleModel, Guid, UserClaimModel, UserRoleModel, UserLoginModel, RoleClaimModel, UserTokenModel>, IRepositoryContext
{
	private readonly AuditingInterceptor _auditingInterceptor;
	private readonly SoftDeletableInterceptor _softDeletableInterceptor;
	private readonly ILoggerService<RepositoryContext> _logger;

	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	/// <summary>
	/// Initializes a new instance of the application repository context class.
	/// </summary>
	/// <param name="dbContextOptions">The database context options.</param>
	/// <param name="auditingInterceptor">The auditing save changes interceptor.</param>
	/// <param name="softDeletableInterceptor">The soft deletable save changes interceptor.</param>
	/// <param name="logger">The logger service.</param>
	public RepositoryContext(
		DbContextOptions<RepositoryContext> dbContextOptions,
		AuditingInterceptor auditingInterceptor,
		SoftDeletableInterceptor softDeletableInterceptor,
		ILoggerService<RepositoryContext> logger
		) : base(dbContextOptions)
	{
		_auditingInterceptor = auditingInterceptor;
		_softDeletableInterceptor = softDeletableInterceptor;
		_logger = logger;

		ChangeTracker.LazyLoadingEnabled = false;
	}

	/// <inheritdoc/>
	[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.ApplyConfigurationsFromAssembly(typeof(IInfrastructureAssemblyMarker).Assembly);
	}

	/// <inheritdoc/>
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.AddInterceptors(_auditingInterceptor, _softDeletableInterceptor);

	/// <inheritdoc/>
	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		int result = default;
		try
		{
			result = await SaveChangesAsync(true, cancellationToken);
		}
		// TODO: What todo else then?
		catch (DbUpdateConcurrencyException ex)
		{
			_logger.Log(LogException, ex);
		}
		return result;
	}
}
