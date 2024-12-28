using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Entities.Todo;

using Infrastructure.Persistence.Converters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Todo;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal sealed class ListEntityConfiguration : AuditedConfiguration<ListEntity>
{
	public override void Configure(EntityTypeBuilder<ListEntity> builder)
	{
		builder.ToHistoryTable("List", SqlSchema.Todo, SqlSchema.History);

		builder.Property(e => e.Title)
			.HasMaxLength(256)
			.IsUnicode();

		builder.Property(p => p.Color)
			.HasConversion<ColorConverter>()
			.IsBinaryColumn(3);

		builder.HasMany(e => e.Items)
			.WithOne(e => e.List)
			.HasForeignKey(e => e.ListId)
			.OnDelete(DeleteBehavior.Cascade)
			.IsRequired();

		builder.HasOne(e => e.User)
			.WithMany(e => e.TodoLists)
			.HasForeignKey(fk => fk.UserId)
			.OnDelete(DeleteBehavior.Cascade)
			.IsRequired();

		base.Configure(builder);
	}
}
