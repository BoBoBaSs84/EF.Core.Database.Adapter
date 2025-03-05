using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;
using BB84.Home.Domain.Entities.Todo;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static BB84.Home.Infrastructure.Common.InfrastructureConstants;

namespace BB84.Home.Infrastructure.Persistence.Configurations.Todo;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal sealed class ItemEntityConfiguration : AuditedConfiguration<ItemEntity>
{
	public override void Configure(EntityTypeBuilder<ItemEntity> builder)
	{
		builder.ToHistoryTable("Item", SqlSchema.Todo, SqlSchema.History);

		builder.Property(e => e.Title)
			.HasMaxLength(256);

		builder.Property(e => e.Note)
			.HasMaxLength(2048);

		base.Configure(builder);
	}
}
