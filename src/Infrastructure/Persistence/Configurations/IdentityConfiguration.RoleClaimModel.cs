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
	internal sealed class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaimModel>
	{
		public void Configure(EntityTypeBuilder<RoleClaimModel> builder) =>
			builder.ToVersionedTable(SqlSchema.Identity, "RoleClaim");
	}
}
