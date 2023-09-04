using Domain.Models.Identity;

using Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static partial class IdentityConfiguration
{
	/// <inheritdoc/>
	internal sealed class UserClaimConfiguration : IEntityTypeConfiguration<UserClaimModel>
	{
		public void Configure(EntityTypeBuilder<UserClaimModel> builder) =>
			builder.ToSytemVersionedTable(SqlSchema.Identity, "UserClaim");
	}
}
