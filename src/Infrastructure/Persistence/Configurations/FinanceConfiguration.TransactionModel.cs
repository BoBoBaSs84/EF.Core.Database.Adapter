using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;

using Domain.Models.Finance;

using Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal static partial class FinanceConfiguration
{
	/// <inheritdoc/>
	internal sealed class TransactionConfiguration : IdentityConfiguration<TransactionModel>
	{
		public override void Configure(EntityTypeBuilder<TransactionModel> builder)
		{
			builder.ToVersionedTable(SqlSchema.Finance, "Transaction");

			builder.Property(e => e.AccountNumber)
				.IsUnicode(false);

			builder.Property(e => e.BankCode)
				.IsUnicode(false);

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
