using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Database.Adapter.Entities.Constants.Sql.Schema;

namespace Database.Adapter.Infrastructure.Configurations.Authentication;

/// <inheritdoc/>
internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
	public void Configure(EntityTypeBuilder<UserRole> builder) =>
		builder.ToSytemVersionedTable(nameof(UserRole), IDENTITY);
}
