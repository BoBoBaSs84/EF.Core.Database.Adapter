using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Finance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal static partial class FinanceConfiguration
{
	/// <inheritdoc/>
	internal sealed class CardConfiguration : AuditedConfiguration<CardModel>
	{
		public override void Configure(EntityTypeBuilder<CardModel> builder)
		{
			builder.ToHistoryTable("Card", SqlSchema.Finance, SqlSchema.History);

			builder.Property(e => e.PAN)
				.IsUnicode(false);

			builder.HasIndex(e => e.PAN)
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
}
