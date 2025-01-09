using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Identity;

/// <summary>
/// The configuration for the <see cref="UserRoleModel"/> entity.
/// </summary>
internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleModel>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<UserRoleModel> builder) =>
		builder.ToHistoryTable("UserRole", SqlSchema.Identity, SqlSchema.History);
}
