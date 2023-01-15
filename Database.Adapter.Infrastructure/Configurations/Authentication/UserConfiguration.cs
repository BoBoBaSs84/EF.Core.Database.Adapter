using Database.Adapter.Entities.Contexts.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Adapter.Infrastructure.Configurations.Authentication;

/// <inheritdoc/>
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{	
	public void Configure(EntityTypeBuilder<User> builder)
	{
	}
}
