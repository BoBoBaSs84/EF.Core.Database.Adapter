using DA.Infrastructure.Extensions;
using DA.Models.Contexts.Finances;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static DA.Models.Constants.Sql.Schema;

namespace DA.Infrastructure.Configurations.Finances;

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
