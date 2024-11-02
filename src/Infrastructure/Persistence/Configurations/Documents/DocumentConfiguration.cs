using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Documents;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Documents;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal sealed class DocumentConfiguration : AuditedConfiguration<Document>
{
	public override void Configure(EntityTypeBuilder<Document> builder)
	{
		builder.ToHistoryTable(nameof(Document), SqlSchema.Documents, SqlSchema.History);

		builder.Property(p => p.Name)
			.HasMaxLength(256)
			.IsRequired()
			.IsUnicode();

		builder.Property(p => p.Directory)
			.HasMaxLength(256)
			.IsRequired()
			.IsUnicode();

		builder.Property(p => p.Flags)
			.IsRequired();

		builder.Property(p => p.CreationTime)
			.IsDateTimeColumn(true)
			.IsRequired();

		builder.Property(p => p.LastWriteTime)
			.IsDateTimeColumn(true)
			.IsRequired(false);

		builder.Property(p => p.LastAccessTime)
			.IsDateTimeColumn(true)
			.IsRequired(false);

		builder.HasOne(e => e.User)
			.WithMany(e => e.Documents)
			.HasForeignKey(fk => fk.UserId)
			.OnDelete(DeleteBehavior.Cascade)
			.IsRequired();

		base.Configure(builder);
	}
}