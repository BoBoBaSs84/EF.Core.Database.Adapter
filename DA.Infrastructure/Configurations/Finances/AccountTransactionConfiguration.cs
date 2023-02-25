using DA.Infrastructure.Extensions;
using DA.Domain.Models.Finances;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static DA.Domain.Constants.Sql.Schema;

namespace DA.Infrastructure.Configurations.Finances;

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
