using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Persistence;

using Domain.Entities.Identity;
using Domain.Models.Identity;

using Infrastructure.Common;
using Infrastructure.Extensions;
using Infrastructure.Persistence.Interceptors;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence;

/// <summary>
/// The application repository context class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityDbContext"/> class.
/// </remarks>
public sealed partial class RepositoryContext : IdentityDbContext<UserModel, RoleModel, Guid, UserClaimModel, UserRoleModel, UserLoginModel, RoleClaimModel, UserTokenModel>, IRepositoryContext
{
	private readonly CustomSaveChangesInterceptor _changesInterceptor;
	private readonly ILoggerService<RepositoryContext> _logger;

	private static readonly Action<ILogger, Exception?> LogException =
	LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	/// <summary>
	/// Initializes a new instance of the application repository context class.
	/// </summary>
	/// <param name="dbContextOptions">The database context options.</param>
	/// <param name="changesInterceptor">The auditable entity save changes interceptor.</param>
	/// <param name="logger">The logger service.</param>
	public RepositoryContext(
		DbContextOptions<RepositoryContext> dbContextOptions,
		CustomSaveChangesInterceptor changesInterceptor,
		ILoggerService<RepositoryContext> logger
		) : base(dbContextOptions)
	{
		_changesInterceptor = changesInterceptor;
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
		=> optionsBuilder.AddInterceptors(_changesInterceptor);

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
