using DA.Domain.Models.Identity;
using DA.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DA.Domain.Constants.Sql.Schema;

namespace DA.Infrastructure.Configurations.Authentication;

/// <inheritdoc/>
internal class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
	public void Configure(EntityTypeBuilder<UserToken> builder) =>
		builder.ToSytemVersionedTable(nameof(UserToken), IDENTITY);
}
