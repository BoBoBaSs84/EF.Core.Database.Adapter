using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Entities.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Identity;

/// <summary>
/// The configuration for the <see cref="RoleClaimModel"/> entity.
/// </summary>
internal sealed class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaimModel>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<RoleClaimModel> builder) =>
		builder.ToHistoryTable("RoleClaim", SqlSchema.Identity, SqlSchema.History);
}
