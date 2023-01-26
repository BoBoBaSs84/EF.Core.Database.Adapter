using DA.Infrastructure.Extensions;
using DA.Models.Contexts.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DA.Models.Constants.Sql.Schema;

namespace DA.Infrastructure.Configurations.Authentication;

/// <inheritdoc/>
internal sealed class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
	public void Configure(EntityTypeBuilder<UserLogin> builder) =>
		builder.ToSytemVersionedTable(nameof(UserLogin), IDENTITY);
}
