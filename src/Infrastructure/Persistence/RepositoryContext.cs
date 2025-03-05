using BB84.EntityFrameworkCore.Repositories.SqlServer.Interceptors;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence;
using BB84.Home.Domain.Entities.Identity;
using BB84.Home.Infrastructure.Common;
using BB84.Home.Infrastructure.Persistence.Generators;
using BB84.Home.Infrastructure.Persistence.Interceptors;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BB84.Home.Infrastructure.Persistence;

/// <summary>
/// The repository context for the application.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="RepositoryContext"/> class.
/// </remarks>
/// <param name="dbContextOptions">The database context options.</param>
/// <param name="userAuditingInterceptor">The auditing save changes interceptor.</param>
/// <param name="softDeletableInterceptor">The soft deletable save changes interceptor.</param>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, repository context.")]
internal sealed partial class RepositoryContext(DbContextOptions<RepositoryContext> dbContextOptions, UserAuditingInterceptor userAuditingInterceptor, SoftDeletableInterceptor softDeletableInterceptor)
	: IdentityDbContext<UserEntity, RoleEntity, Guid, UserClaimEntity, UserRoleEntity, UserLoginModel, RoleClaimEntity, UserTokenModel>(dbContextOptions), IRepositoryContext
{
	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.HasDefaultSchema(InfrastructureConstants.SqlSchema.Private);
		builder.ApplyConfigurationsFromAssembly(typeof(IInfrastructureAssemblyMarker).Assembly);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);

		optionsBuilder.AddInterceptors(userAuditingInterceptor, softDeletableInterceptor);
		optionsBuilder.ReplaceService<IMigrationsSqlGenerator, RepositorySqlGenerator>();
		optionsBuilder.ConfigureWarnings(x => x.Ignore(RelationalEventId.PendingModelChangesWarning));
	}
}
