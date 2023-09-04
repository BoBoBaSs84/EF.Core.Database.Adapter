using Domain.Models.Finance;

using Infrastructure.Extensions;
using Infrastructure.Persistence.Configurations.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal static partial class FinanceConfiguration
{
	/// <inheritdoc/>
	internal sealed class AccountTransactionConfiguration : CompositeBaseConfiguration<AccountTransactionModel>
	{
		public override void Configure(EntityTypeBuilder<AccountTransactionModel> builder)
		{
			builder.ToSytemVersionedTable(SqlSchema.Finance, "AccountTransaction");

			builder.HasKey(e => new { e.AccountId, e.TransactionId })
				.IsClustered(false);

			base.Configure(builder);
		}
	}
}
