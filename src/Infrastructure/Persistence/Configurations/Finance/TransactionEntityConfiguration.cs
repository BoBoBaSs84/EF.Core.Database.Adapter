using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;
using BB84.Home.Domain.Entities.Finance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Finance;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal sealed class TransactionEntityConfiguration : AuditedConfiguration<TransactionEntity>
{
	public override void Configure(EntityTypeBuilder<TransactionEntity> builder)
	{
		builder.ToHistoryTable("Transaction", SqlSchema.Finance, SqlSchema.History);

		builder.Property(p => p.BookingDate)
			.IsDateColumn();

		builder.Property(p => p.ValueDate)
			.IsDateColumn();

		builder.Property(p => p.PostingText)
			.HasMaxLength(100);

		builder.Property(p => p.ClientBeneficiary)
			.HasMaxLength(250);

		builder.Property(p => p.Purpose)
			.HasMaxLength(4000);

		builder.Property(p => p.AccountNumber)
			.HasMaxLength(25)
			.IsUnicode(false);

		builder.Property(p => p.BankCode)
			.HasMaxLength(25)
			.IsUnicode(false);

		builder.Property(p => p.AmountEur)
			.IsMoneyColumn();

		builder.Property(p => p.CreditorId)
			.HasMaxLength(25);

		builder.Property(p => p.MandateReference)
			.HasMaxLength(50);

		builder.Property(p => p.CustomerReference)
			.HasMaxLength(50);

		builder.HasMany(e => e.AccountTransactions)
			.WithOne(e => e.Transaction)
			.HasForeignKey(e => e.TransactionId)
			.OnDelete(DeleteBehavior.Cascade)
			.IsRequired();

		builder.HasMany(e => e.CardTransactions)
			.WithOne(e => e.Transaction)
			.HasForeignKey(e => e.TransactionId)
			.OnDelete(DeleteBehavior.Cascade)
			.IsRequired();

		base.Configure(builder);
	}
}
