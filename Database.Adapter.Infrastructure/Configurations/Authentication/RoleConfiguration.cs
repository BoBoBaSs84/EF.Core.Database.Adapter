using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static Database.Adapter.Entities.Constants.SqlConstants.SqlSchema;

namespace Database.Adapter.Infrastructure.Configurations.Authentication;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.ToSytemVersionedTable(nameof(Role), IDENTITY, HISTORY);

		builder.HasMany(e => e.UserRoles)
			.WithOne(e => e.Role)
			.HasForeignKey(ur => ur.RoleId)
			.IsRequired(true);

		builder.HasMany(e => e.RoleClaims)
			.WithOne(e => e.Role)
			.HasForeignKey(rc => rc.RoleId)
			.IsRequired(true);
	}
}
