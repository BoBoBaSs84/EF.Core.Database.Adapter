using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Database.Adapter.Entities.Constants.Sql.Schema;

namespace Database.Adapter.Infrastructure.Configurations.Authentication;

/// <inheritdoc/>
internal class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
	public void Configure(EntityTypeBuilder<UserToken> builder) =>
		builder.ToSytemVersionedTable(nameof(UserToken), IDENTITY);
}
