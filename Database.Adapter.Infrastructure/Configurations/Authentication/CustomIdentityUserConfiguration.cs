using Database.Adapter.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Infrastructure.Configurations.Authentication;

/// <inheritdoc/>
internal sealed class CustomIdentityUserConfiguration : IEntityTypeConfiguration<CustomIdentityUser>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<CustomIdentityUser> builder)
	{
		_ = builder.Property(p => p.FirstName)
			.HasMaxLength(SqlStringLength.MAX_LENGHT_64)
			.IsRequired(true);

		_ = builder.Property(p => p.MiddleName)
			.HasMaxLength(SqlStringLength.MAX_LENGHT_64)
			.IsRequired(false);

		_ = builder.Property(p => p.LastName)
			.HasMaxLength(SqlStringLength.MAX_LENGHT_64)
			.IsRequired(true);

		_ = builder.Property(p => p.DateOfBirth)
			.HasMaxLength(SqlStringLength.MAX_LENGHT_64)
			.IsRequired(false);

		_ = builder.Property(p => p.Preferences)
			.HasMaxLength(SqlStringLength.MAX_LENGHT_4092)
			.IsRequired(false);
	}
}
