using Database.Adapter.Entities.Contexts.Finances;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static Database.Adapter.Entities.Constants.Sql.Schema;

namespace Database.Adapter.Infrastructure.Configurations.Finances;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class AccountTransactionConfiguration : IEntityTypeConfiguration<AccountTransaction>
{
	public void Configure(EntityTypeBuilder<AccountTransaction> builder)
	{
		builder.ToSytemVersionedTable(nameof(AccountTransaction), FINANCE);

		builder.HasKey(e => new { e.AccountId, e.TransactionId })
			.IsClustered(false);
	}
}
