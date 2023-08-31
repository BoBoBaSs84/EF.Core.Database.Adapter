using Domain.Entities.Finance;

using Infrastructure.Extensions;
using Infrastructure.Persistence.Configurations.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations.Finance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal static class FinanceConfiguration
{
	/// <inheritdoc/>
	internal sealed class AccountConfiguration : IdentityTypeBaseConfiguration<Account>
	{
		public override void Configure(EntityTypeBuilder<Account> builder)
		{
			builder.ToSytemVersionedTable(SqlSchema.FINANCE);

			builder.Property(e => e.IBAN)
				.IsUnicode(false);

			builder.HasIndex(e => new { e.IBAN })
				.IsClustered(false)
				.IsUnique(true);

			builder.HasMany(e => e.AccountUsers)
				.WithOne(e => e.Account)
				.HasForeignKey(e => e.AccountId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasMany(e => e.AccountTransactions)
				.WithOne(e => e.Account)
				.HasForeignKey(e => e.AccountId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasMany(e => e.Cards)
				.WithOne(e => e.Account)
				.HasForeignKey(e => e.AccountId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			base.Configure(builder);
		}
	}

	/// <inheritdoc/>
	internal sealed class AccountTransactionConfiguration : CompositeTypeBaseConfiguration<AccountTransaction>
	{
		public override void Configure(EntityTypeBuilder<AccountTransaction> builder)
		{
			builder.ToSytemVersionedTable(SqlSchema.FINANCE);

			builder.HasKey(e => new { e.AccountId, e.TransactionId })
				.IsClustered(false);

			base.Configure(builder);
		}
	}

	/// <inheritdoc/>
	internal sealed class AccountUserConfiguration : CompositeTypeBaseConfiguration<AccountUser>
	{
		public override void Configure(EntityTypeBuilder<AccountUser> builder)
		{
			builder.ToSytemVersionedTable(SqlSchema.FINANCE);

			builder.HasKey(e => new { e.AccountId, e.UserId })
				.IsClustered(false);

			base.Configure(builder);
		}
	}

	/// <inheritdoc/>
	internal sealed class CardConfiguration : IdentityTypeBaseConfiguration<Card>
	{
		public override void Configure(EntityTypeBuilder<Card> builder)
		{
			builder.ToSytemVersionedTable(SqlSchema.FINANCE);

			builder.Property(e => e.PAN)
				.IsUnicode(false);

			builder.HasIndex(e => new { e.PAN })
				.IsClustered(false)
				.IsUnique(true);

			builder.HasMany(e => e.CardTransactions)
				.WithOne(e => e.Card)
				.HasForeignKey(e => e.CardId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			base.Configure(builder);
		}
	}

	/// <inheritdoc/>
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

	/// <inheritdoc/>
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
