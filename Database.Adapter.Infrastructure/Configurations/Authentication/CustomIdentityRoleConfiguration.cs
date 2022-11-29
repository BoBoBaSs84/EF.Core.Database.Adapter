using Database.Adapter.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Infrastructure.Configurations.Authentication;

/// <inheritdoc/>
internal sealed class CustomIdentityRoleConfiguration : IEntityTypeConfiguration<CustomIdentityRole>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<CustomIdentityRole> builder)
	{
		_ = builder.Property(p => p.Description)
			.HasMaxLength(SqlStringLength.MAX_LENGHT_256)
			.IsRequired(false);
	}
}
