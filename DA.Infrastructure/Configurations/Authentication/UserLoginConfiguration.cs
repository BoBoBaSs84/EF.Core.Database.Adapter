using DA.Domain.Models.Identity;
using DA.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DA.Domain.Constants.Sql.Schema;

namespace DA.Infrastructure.Configurations.Authentication;

/// <inheritdoc/>
internal sealed class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
	public void Configure(EntityTypeBuilder<UserLogin> builder) =>
		builder.ToSytemVersionedTable(nameof(UserLogin), IDENTITY);
}
