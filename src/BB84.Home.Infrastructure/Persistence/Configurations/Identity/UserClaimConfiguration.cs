using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;
using BB84.Home.Domain.Entities.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static BB84.Home.Infrastructure.Common.InfrastructureConstants;

namespace BB84.Home.Infrastructure.Persistence.Configurations.Identity;

/// <summary>
/// The configuration for the <see cref="UserClaimEntity"/> entity.
/// </summary>
internal sealed class UserClaimConfiguration : IEntityTypeConfiguration<UserClaimEntity>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<UserClaimEntity> builder) =>
		builder.ToHistoryTable("UserClaim", SqlSchema.Identity, SqlSchema.History);
}
