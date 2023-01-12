using Database.Adapter.Entities.Contexts.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Adapter.Infrastructure.Configurations.Authentication;

/// <inheritdoc/>
internal sealed class CustomIdentityUserConfiguration : IEntityTypeConfiguration<CustomIdentityUser>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<CustomIdentityUser> builder)
	{
	}
}
