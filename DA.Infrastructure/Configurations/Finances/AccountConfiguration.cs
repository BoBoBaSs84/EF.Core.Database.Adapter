using DA.Infrastructure.Extensions;
using DA.Domain.Models.Finances;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static DA.Domain.Constants.Sql.Schema;

namespace DA.Infrastructure.Configurations.Finances;

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
