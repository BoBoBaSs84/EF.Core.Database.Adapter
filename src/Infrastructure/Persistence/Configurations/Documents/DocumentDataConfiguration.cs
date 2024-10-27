using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Documents;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Documents;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal sealed class DocumentDataConfiguration : AuditedConfiguration<DocumentData>
{
	public override void Configure(EntityTypeBuilder<DocumentData> builder)
	{
		builder.ToHistoryTable(nameof(DocumentData), SqlSchema.Documents, SqlSchema.History);

		builder.HasKey(k => new { k.DocumentId, k.DataId })
			.IsClustered(false);

		builder.HasOne(e => e.Document)
			.WithMany(e => e.DocumentDatas)
			.OnDelete(DeleteBehavior.Cascade)
			.IsRequired();

		builder.HasOne(e => e.Data)
			.WithMany(e => e.DocumentDatas)
			.OnDelete(DeleteBehavior.Cascade)
			.IsRequired();

		base.Configure(builder);
	}
}
