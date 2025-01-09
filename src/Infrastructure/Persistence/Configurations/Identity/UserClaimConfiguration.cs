using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Entities.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Identity;

/// <summary>
/// The configuration for the <see cref="UserClaimModel"/> entity.
/// </summary>
internal sealed class UserClaimConfiguration : IEntityTypeConfiguration<UserClaimModel>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<UserClaimModel> builder) =>
		builder.ToHistoryTable("UserClaim", SqlSchema.Identity, SqlSchema.History);
}
