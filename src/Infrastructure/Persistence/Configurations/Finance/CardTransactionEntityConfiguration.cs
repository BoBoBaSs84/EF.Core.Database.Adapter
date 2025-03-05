using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;
using BB84.Home.Domain.Entities.Finance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Finance;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal sealed class CardTransactionEntityConfiguration : AuditedCompositeConfiguration<CardTransactionEntity>
{
	public override void Configure(EntityTypeBuilder<CardTransactionEntity> builder)
	{
		builder.ToHistoryTable("CardTransaction", SqlSchema.Finance, SqlSchema.History);

		builder.HasKey(e => new { e.CardId, e.TransactionId })
			.IsClustered(false);

		base.Configure(builder);
	}
}
