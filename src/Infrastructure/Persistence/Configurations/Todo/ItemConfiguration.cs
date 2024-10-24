using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Todo;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Todo;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal sealed class ItemConfiguration : AuditedConfiguration<Item>
{
	public override void Configure(EntityTypeBuilder<Item> builder)
	{
		builder.ToHistoryTable(nameof(Item), SqlSchema.Todo, SqlSchema.History);

		builder.Property(e => e.Title)
			.HasMaxLength(256);

		builder.Property(e => e.Note)
			.HasMaxLength(2048);

		base.Configure(builder);
	}
}
