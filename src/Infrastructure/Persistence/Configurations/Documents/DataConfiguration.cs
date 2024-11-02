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

		builder.Property(p => p.MD5Hash)
			.IsBinaryColumn(16)
			.IsRequired();

		builder.HasIndex(i => i.MD5Hash)
			.IsClustered(false)
			.IsUnique(true);

		builder.Property(p => p.Length)
			.IsRequired();

		builder.Property(p => p.Content)
			.IsVarbinaryColumn();

		builder.HasMany(e => e.Documents)
			.WithOne(e => e.Data)
			.HasForeignKey(fk => fk.DataId)
			.OnDelete(DeleteBehavior.Cascade)
			.IsRequired();

		base.Configure(builder);
	}
}
