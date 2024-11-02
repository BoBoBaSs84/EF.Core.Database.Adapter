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
/// <remarks>
/// Initializes a new instance of the <see cref="RepositoryContext"/> class.
/// </remarks>
/// <param name="dbContextOptions">The database context options.</param>
/// <param name="auditingInterceptor">The auditing save changes interceptor.</param>
/// <param name="softDeletableInterceptor">The soft deletable save changes interceptor.</param>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, repository context.")]
internal sealed partial class RepositoryContext(DbContextOptions<RepositoryContext> dbContextOptions, AuditingInterceptor auditingInterceptor, SoftDeletableInterceptor softDeletableInterceptor)
	: IdentityDbContext<UserModel, RoleModel, Guid, UserClaimModel, UserRoleModel, UserLoginModel, RoleClaimModel, UserTokenModel>(dbContextOptions), IRepositoryContext
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

		optionsBuilder.AddInterceptors(auditingInterceptor, softDeletableInterceptor);
		optionsBuilder.ReplaceService<IMigrationsSqlGenerator, RepositorySqlGenerator>();
	}
}
