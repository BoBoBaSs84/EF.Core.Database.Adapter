using Domain.Entities.Finance;
using Infrastructure.Extensions;
using Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations.Finance;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class TransactionConfiguration : IdentityTypeBaseConfiguration<Transaction>
{
	public override void Configure(EntityTypeBuilder<Transaction> builder)
	{
		builder.ToSytemVersionedTable(SqlSchema.FINANCE);

		builder.Property(e => e.AccountNumber)
			.IsUnicode(false);

		builder.Property(e => e.BankCode)
			.IsUnicode(false);

		builder.HasMany(e => e.AccountTransactions)
			.WithOne(e => e.Transaction)
			.HasForeignKey(e => e.TransactionId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired();

		builder.HasMany(e => e.CardTransactions)
			.WithOne(e => e.Transaction)
			.HasForeignKey(e => e.TransactionId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired();

		base.Configure(builder);
	}
}