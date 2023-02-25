using DA.Infrastructure.Extensions;
using DA.Domain.Models.Finances;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static DA.Domain.Constants.Sql.Schema;

namespace DA.Infrastructure.Configurations.Finances;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class CardConfiguration : IEntityTypeConfiguration<Card>
{
	public void Configure(EntityTypeBuilder<Card> builder)
	{
		builder.ToSytemVersionedTable(nameof(Card), FINANCE);

		builder.HasKey(e => e.Id)
			.IsClustered(false);

		builder.HasMany(e => e.CardTransactions)
			.WithOne(e => e.Card)
			.HasForeignKey(e => e.CardId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired(true);
	}
}
