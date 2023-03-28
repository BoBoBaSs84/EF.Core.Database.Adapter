using Domain.Entities.Finance;
using Infrastructure.Extensions;
using Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations.Finance;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class CardTransactionConfiguration : CompositeTypeBaseConfiguration<CardTransaction>
{
	public override void Configure(EntityTypeBuilder<CardTransaction> builder)
	{
		builder.ToSytemVersionedTable(SqlSchema.FINANCE);

		builder.HasKey(e => new { e.CardId, e.TransactionId })
			.IsClustered(false);

		base.Configure(builder);
	}
}
