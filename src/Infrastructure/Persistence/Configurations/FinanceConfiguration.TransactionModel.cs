using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Finance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlDataType = Domain.Constants.DomainConstants.Sql.DataType;
using SqlMaxLength = Domain.Constants.DomainConstants.Sql.MaxLength;
using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

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
				.HasColumnType(SqlDataType.DATE);

			builder.Property(p => p.ValueDate)
				.HasColumnType(SqlDataType.DATE);

			builder.Property(p => p.PostingText)
				.HasMaxLength(SqlMaxLength.MAX_100);

			builder.Property(p => p.ClientBeneficiary)
				.HasMaxLength(SqlMaxLength.MAX_250);

			builder.Property(p => p.Purpose)
				.HasMaxLength(SqlMaxLength.MAX_4000);

			builder.Property(p => p.AccountNumber)
				.HasMaxLength(SqlMaxLength.MAX_25)
				.IsUnicode(false);

			builder.Property(p => p.BankCode)
				.HasMaxLength(SqlMaxLength.MAX_25)
				.IsUnicode(false);

			builder.Property(p => p.AmountEur)
				.HasColumnType(SqlDataType.MONEY);

			builder.Property(p => p.CreditorId)
				.HasMaxLength(SqlMaxLength.MAX_25);

			builder.Property(p => p.MandateReference)
				.HasMaxLength(SqlMaxLength.MAX_50);

			builder.Property(p => p.CustomerReference)
				.HasMaxLength(SqlMaxLength.MAX_50);

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
