using DA.Infrastructure.Extensions;
using DA.Models.Contexts.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DA.Models.Constants.Sql.Schema;

namespace DA.Infrastructure.Configurations.Authentication;

/// <inheritdoc/>
internal class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
	public void Configure(EntityTypeBuilder<UserToken> builder) =>
		builder.ToSytemVersionedTable(nameof(UserToken), IDENTITY);
}
