using Domain.Entities.Finance;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static Domain.Constants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static class FinanceConfiguration
{
	/// <inheritdoc/>
	internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
	{
		public void Configure(EntityTypeBuilder<Account> builder)
		{
			builder.ToSytemVersionedTable(nameof(Account), FINANCE);

			builder.HasKey(e => e.Id)
				.IsClustered(false);

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
		}
	}

	/// <inheritdoc/>
	internal sealed class AccountTransactionConfiguration : IEntityTypeConfiguration<AccountTransaction>
	{
		public void Configure(EntityTypeBuilder<AccountTransaction> builder)
		{
			builder.ToSytemVersionedTable(nameof(AccountTransaction), FINANCE);

			builder.HasKey(e => new { e.AccountId, e.TransactionId })
				.IsClustered(false);
		}
	}

	/// <inheritdoc/>
	internal sealed class AccountUserConfiguration : IEntityTypeConfiguration<AccountUser>
	{
		public void Configure(EntityTypeBuilder<AccountUser> builder)
		{
			builder.ToSytemVersionedTable(nameof(AccountUser), FINANCE);

			builder.HasKey(e => new { e.AccountId, e.UserId })
				.IsClustered(false);
		}
	}

	/// <inheritdoc/>
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

	/// <inheritdoc/>
	internal sealed class CardTransactionConfiguration : IEntityTypeConfiguration<CardTransaction>
	{
		public void Configure(EntityTypeBuilder<CardTransaction> builder)
		{
			builder.ToSytemVersionedTable(nameof(CardTransaction), FINANCE);

			builder.HasKey(e => new { e.CardId, e.TransactionId })
				.IsClustered(false);
		}
	}

	/// <inheritdoc/>
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
}
