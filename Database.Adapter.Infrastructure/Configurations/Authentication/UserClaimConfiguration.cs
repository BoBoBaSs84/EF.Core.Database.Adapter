using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Database.Adapter.Entities.Constants.Sql.Schema;

namespace Database.Adapter.Infrastructure.Configurations.Authentication;

/// <inheritdoc/>
internal sealed class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
	public void Configure(EntityTypeBuilder<UserClaim> builder) =>
		builder.ToSytemVersionedTable(nameof(UserClaim), IDENTITY);
}
