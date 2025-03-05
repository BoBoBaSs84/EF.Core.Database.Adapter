using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;
using BB84.Home.Domain.Entities.Documents;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Documents;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal sealed class DataEntityConfiguration : AuditedConfiguration<DataEntity>
{
	public override void Configure(EntityTypeBuilder<DataEntity> builder)
	{
		builder.ToHistoryTable("Data", SqlSchema.Documents, SqlSchema.History);

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
