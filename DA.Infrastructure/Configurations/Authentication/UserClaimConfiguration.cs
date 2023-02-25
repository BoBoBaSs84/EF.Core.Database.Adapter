using DA.Domain.Models.Identity;
using DA.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DA.Domain.Constants.Sql.Schema;

namespace DA.Infrastructure.Configurations.Authentication;

/// <inheritdoc/>
internal sealed class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
	public void Configure(EntityTypeBuilder<UserClaim> builder) =>
		builder.ToSytemVersionedTable(nameof(UserClaim), IDENTITY);
}
