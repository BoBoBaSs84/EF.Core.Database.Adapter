using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Finance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Common.Constants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal static partial class FinanceConfiguration
{
	/// <inheritdoc/>
	internal sealed class CardTransactionConfiguration : AuditedCompositeConfiguration<CardTransactionModel>
	{
		public override void Configure(EntityTypeBuilder<CardTransactionModel> builder)
		{
			builder.ToHistoryTable("CardTransaction", SqlSchema.Finance, SqlSchema.History);

			builder.HasKey(e => new { e.CardId, e.TransactionId })
				.IsClustered(false);

			base.Configure(builder);
		}
	}
}
