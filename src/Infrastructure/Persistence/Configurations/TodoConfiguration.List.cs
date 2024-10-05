using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Todo;

using Infrastructure.Persistence.Converters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal static partial class TodoConfiguration
{
	/// <inheritdoc/>
	internal sealed class TodoListConfiguration : AuditedConfiguration<List>
	{
		public override void Configure(EntityTypeBuilder<List> builder)
		{
			builder.ToHistoryTable("List", SqlSchema.Todo, SqlSchema.History);

			builder.Property(e => e.Title)
				.HasMaxLength(256)
				.IsUnicode();

			builder.Property(p => p.Color)
				.HasConversion<ColorConverter>()
				.HasColumnType("varbinary(3)");

			builder.HasMany(e => e.Items)
				.WithOne(e => e.List)
				.HasForeignKey(e => e.ListId)
				.OnDelete(DeleteBehavior.Cascade);

			base.Configure(builder);
		}
	}
}
