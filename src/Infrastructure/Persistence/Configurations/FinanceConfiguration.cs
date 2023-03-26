using Domain.Entities.Finance;
using Infrastructure.Extensions;
using Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal static class FinanceConfiguration
{
	/// <inheritdoc/>
	internal sealed class AccountConfiguration : FullAuditTypeBaseConfiguration<Account>
	{
		public override void Configure(EntityTypeBuilder<Account> builder)
		{
			builder.Property(e => e.IBAN)
				.IsUnicode(false);

			builder.HasIndex(e => e.IBAN)
				.IsClustered(false)
				.IsUnique(true);

			builder.HasMany(e => e.AccountUsers)
				.WithOne(e => e.Account)
				.HasForeignKey(e => e.AccountId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(true);

			builder.HasMany(e => e.AccountTransactions)
				.WithOne(e => e.Account)
				.HasForeignKey(e => e.AccountId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(true);

			builder.HasMany(e => e.Cards)
				.WithOne(e => e.Account)
				.HasForeignKey(e => e.AccountId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(true);

			Configure(builder, SqlSchema.FINANCE);
		}
	}

	/// <inheritdoc/>
	internal sealed class AccountTransactionConfiguration : IEntityTypeConfiguration<AccountTransaction>
	{
		public void Configure(EntityTypeBuilder<AccountTransaction> builder)
		{
			builder.ToSytemVersionedTable(nameof(AccountTransaction), SqlSchema.FINANCE);

			builder.HasKey(e => new { e.AccountId, e.TransactionId })
				.IsClustered(false);
		}
	}

	/// <inheritdoc/>
	internal sealed class AccountUserConfiguration : IEntityTypeConfiguration<AccountUser>
	{
		public void Configure(EntityTypeBuilder<AccountUser> builder)
		{
			builder.ToSytemVersionedTable(nameof(AccountUser), SqlSchema.FINANCE);

			builder.HasKey(e => new { e.AccountId, e.UserId })
				.IsClustered(false);
		}
	}

	/// <inheritdoc/>
	internal sealed class CardConfiguration : FullAuditTypeBaseConfiguration<Card>
	{
		public override void Configure(EntityTypeBuilder<Card> builder)
		{
			builder.Property(e => e.PAN)
				.IsUnicode(false);

			builder.HasIndex(e => e.PAN)
				.IsClustered(false)
				.IsUnique(true);

			builder.HasMany(e => e.CardTransactions)
				.WithOne(e => e.Card)
				.HasForeignKey(e => e.CardId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(true);

			Configure(builder, SqlSchema.FINANCE);
		}
	}

	/// <inheritdoc/>
	internal sealed class CardTransactionConfiguration : IEntityTypeConfiguration<CardTransaction>
	{
		public void Configure(EntityTypeBuilder<CardTransaction> builder)
		{
			builder.ToSytemVersionedTable(nameof(CardTransaction), SqlSchema.FINANCE);

			builder.HasKey(e => new { e.CardId, e.TransactionId })
				.IsClustered(false);
		}
	}

	/// <inheritdoc/>
	internal sealed class TransactionConfiguration : FullAuditTypeBaseConfiguration<Transaction>
	{
		public override void Configure(EntityTypeBuilder<Transaction> builder)
		{
			builder.Property(e => e.AccountNumber)
				.IsUnicode(false);

			builder.Property(e => e.BankCode)
				.IsUnicode(false);

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

			base.Configure(builder, SqlSchema.FINANCE);
		}
	}
}
