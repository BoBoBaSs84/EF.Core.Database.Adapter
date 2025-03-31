using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;
using BB84.Home.Domain.Entities.Finance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static BB84.Home.Infrastructure.Common.InfrastructureConstants;

namespace BB84.Home.Infrastructure.Persistence.Configurations.Finance;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal sealed class CardEntityConfiguration : AuditedConfiguration<CardEntity>
{
	public override void Configure(EntityTypeBuilder<CardEntity> builder)
	{
		builder.ToHistoryTable("Card", SqlSchema.Finance, SqlSchema.History);

		builder.HasIndex(i => i.PAN)
			.IsClustered(false)
			.IsUnique(true);

		builder.Property(p => p.PAN)
			.HasMaxLength(25)
			.IsUnicode(false);

		builder.Property(p => p.ValidUntil)
			.IsDateColumn();

		builder.HasMany(e => e.Transactions)
			.WithOne(e => e.Card)
			.HasForeignKey(e => e.CardId)
			.OnDelete(DeleteBehavior.Cascade)
			.IsRequired();

		base.Configure(builder);
	}
}
