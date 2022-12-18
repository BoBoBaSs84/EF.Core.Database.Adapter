using Database.Adapter.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Adapter.Infrastructure.Configurations.Authentication;

/// <inheritdoc/>
internal sealed class CustomIdentityRoleConfiguration : IEntityTypeConfiguration<CustomIdentityRole>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<CustomIdentityRole> builder)
	{
	}
}
