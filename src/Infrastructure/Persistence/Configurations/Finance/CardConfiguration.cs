using Domain.Entities.Finance;
using Infrastructure.Extensions;
using Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations.Finance;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal sealed class CardConfiguration : IdentityTypeBaseConfiguration<Card>
{
	public override void Configure(EntityTypeBuilder<Card> builder)
	{
		builder.ToSytemVersionedTable(SqlSchema.FINANCE);

		builder.Property(e => e.PAN)
			.IsUnicode(false);

		builder.HasIndex(e => new { e.PAN })
			.IsClustered(false)
			.IsUnique(true);

		builder.HasMany(e => e.CardTransactions)
			.WithOne(e => e.Card)
			.HasForeignKey(e => e.CardId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired();

		base.Configure(builder);
	}
}
