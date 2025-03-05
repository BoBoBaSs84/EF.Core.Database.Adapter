using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;
using BB84.Home.Domain.Entities.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Identity;

/// <summary>
/// The configuration for the <see cref="UserRoleEntity"/> entity.
/// </summary>
internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleEntity>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<UserRoleEntity> builder) =>
		builder.ToHistoryTable("UserRole", SqlSchema.Identity, SqlSchema.History);
}
