using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Documents;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Documents;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal sealed class DataConfiguration : AuditedConfiguration<Data>
{
	public override void Configure(EntityTypeBuilder<Data> builder)
	{
		builder.ToHistoryTable(nameof(Data), SqlSchema.Documents, SqlSchema.History);

		builder.Property(p => p.RawData)
			.IsVarbinaryColumn();

		builder.HasOne(e => e.Document)
			.WithOne(e => e.Data)
			.HasForeignKey<Data>(e => e.DocumentId)
			.OnDelete(DeleteBehavior.Cascade)
			.IsRequired();

		base.Configure(builder);
	}
}
