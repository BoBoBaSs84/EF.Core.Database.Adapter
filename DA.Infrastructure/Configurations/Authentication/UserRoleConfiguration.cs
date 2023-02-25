using DA.Domain.Models.Identity;
using DA.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DA.Domain.Constants.Sql.Schema;

namespace DA.Infrastructure.Configurations.Authentication;

/// <inheritdoc/>
internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
	public void Configure(EntityTypeBuilder<UserRole> builder) =>
		builder.ToSytemVersionedTable(nameof(UserRole), IDENTITY);
}
