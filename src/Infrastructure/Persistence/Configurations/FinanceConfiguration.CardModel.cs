using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Finance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

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

			builder.HasIndex(i => i.PAN)
				.IsClustered(false)
				.IsUnique(true);

			builder.Property(p => p.PAN)
				.HasMaxLength(25)
				.IsUnicode(false);

			builder.Property(p => p.ValidUntil)
				.HasDateColumnType();

			builder.HasMany(e => e.Transactions)
				.WithOne(e => e.Card)
				.HasForeignKey(e => e.CardId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			base.Configure(builder);
		}
	}
}
