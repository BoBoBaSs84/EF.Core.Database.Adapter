using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static partial class IdentityConfiguration
{
	/// <inheritdoc/>
	internal sealed class UserClaimConfiguration : IEntityTypeConfiguration<UserClaimModel>
	{
		public void Configure(EntityTypeBuilder<UserClaimModel> builder) =>
			builder.ToHistoryTable("UserClaim", SqlSchema.Identity, SqlSchema.History);
	}
}
