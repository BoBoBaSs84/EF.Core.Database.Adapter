using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Finance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal static partial class FinanceConfiguration
{
	/// <inheritdoc/>
	internal sealed class TransactionConfiguration : AuditedConfiguration<TransactionModel>
	{
		public override void Configure(EntityTypeBuilder<TransactionModel> builder)
		{
			builder.ToHistoryTable("Transaction", SqlSchema.Finance, SqlSchema.History);

			builder.Property(p => p.BookingDate)
				.HasDateColumnType();

			builder.Property(p => p.ValueDate)
				.HasDateColumnType();

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
				.HasMoneyColumnType();

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
}
