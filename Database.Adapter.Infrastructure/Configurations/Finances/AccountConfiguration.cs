using Database.Adapter.Entities.Contexts.Finances;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static Database.Adapter.Entities.Constants.Sql.Schema;

namespace Database.Adapter.Infrastructure.Configurations.Finances;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
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
