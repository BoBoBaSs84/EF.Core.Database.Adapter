using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Entities.Documents;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Documents;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal sealed class ExtensionEntityConfiguration : AuditedConfiguration<ExtensionEntity>
{
	public override void Configure(EntityTypeBuilder<ExtensionEntity> builder)
	{
		builder.ToHistoryTable("Extension", SqlSchema.Documents, SqlSchema.History);

		builder.Property(p => p.Name)
			.HasMaxLength(256)
			.IsRequired();

		builder.HasIndex(p => p.Name)
			.IsClustered(false)
			.IsUnique();

		builder.Property(p => p.MimeType)
			.HasMaxLength(256)
			.IsRequired(false);

		builder.HasMany(e => e.Documents)
			.WithOne(e => e.Extension)
			.HasForeignKey(fk => fk.ExtensionId)
			.OnDelete(DeleteBehavior.Cascade)
			.IsRequired();

		base.Configure(builder);
	}
}
