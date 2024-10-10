using Application.Interfaces.Infrastructure.Persistence;

using BB84.EntityFrameworkCore.Repositories.SqlServer.Interceptors;

using Domain.Models.Identity;

using Infrastructure.Common;
using Infrastructure.Persistence.Generators;
using Infrastructure.Persistence.Interceptors;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence;

/// <summary>
/// The repository context for the application.
/// </summary>
internal sealed partial class RepositoryContext
	: IdentityDbContext<UserModel, RoleModel, Guid, UserClaimModel, UserRoleModel, UserLoginModel, RoleClaimModel, UserTokenModel>, IRepositoryContext
{
	private readonly AuditingInterceptor _auditingInterceptor;
	private readonly SoftDeletableInterceptor _softDeletableInterceptor;

	/// <summary>
	/// Initializes a new instance of the <see cref="RepositoryContext"/> class.
	/// </summary>
	/// <param name="dbContextOptions">The database context options.</param>
	/// <param name="auditingInterceptor">The auditing save changes interceptor.</param>
	/// <param name="softDeletableInterceptor">The soft deletable save changes interceptor.</param>
	public RepositoryContext(DbContextOptions<RepositoryContext> dbContextOptions, AuditingInterceptor auditingInterceptor, SoftDeletableInterceptor softDeletableInterceptor)
		: base(dbContextOptions)
	{
		_auditingInterceptor = auditingInterceptor;
		_softDeletableInterceptor = softDeletableInterceptor;

		ChangeTracker.LazyLoadingEnabled = false;
	}

	/// <inheritdoc/>
	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.HasDefaultSchema(InfrastructureConstants.SqlSchema.Private);
		builder.ApplyConfigurationsFromAssembly(typeof(IInfrastructureAssemblyMarker).Assembly);
	}

	/// <inheritdoc/>
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);

		optionsBuilder.AddInterceptors(_auditingInterceptor, _softDeletableInterceptor);
		optionsBuilder.ReplaceService<IMigrationsSqlGenerator, RepositorySqlGenerator>();		
	}
}
