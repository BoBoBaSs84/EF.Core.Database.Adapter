using Database.Adapter.Entities.Contexts.Finances;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static Database.Adapter.Entities.Constants.SqlConstants.SqlSchema;

namespace Database.Adapter.Infrastructure.Configurations.Finances;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class CardTransactionConfiguration : IEntityTypeConfiguration<CardTransaction>
{
	public void Configure(EntityTypeBuilder<CardTransaction> builder)
	{
		builder.ToSytemVersionedTable(nameof(CardTransaction), FINANCE);

		builder.HasKey(e => new { e.CardId, e.TransactionId })
			.IsClustered(false);
	}
}
