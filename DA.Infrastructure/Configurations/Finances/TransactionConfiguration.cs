using DA.Domain.Models.Finances;
using DA.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static DA.Domain.Constants.Sql.Schema;

namespace DA.Infrastructure.Configurations.Finances;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
	public void Configure(EntityTypeBuilder<Transaction> builder)
	{
		builder.ToSytemVersionedTable(nameof(Transaction), FINANCE);

		builder.HasKey(e => e.Id)
			.IsClustered(false);

		builder.HasMany(e => e.AccountTransactions)
			.WithOne(e => e.Transaction)
			.HasForeignKey(e => e.TransactionId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired(true);

		builder.HasMany(e => e.CardTransactions)
			.WithOne(e => e.Transaction)
			.HasForeignKey(e => e.TransactionId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired(true);
	}
}
