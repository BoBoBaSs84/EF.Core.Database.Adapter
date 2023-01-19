using Database.Adapter.Entities.Contexts.MasterData;
using Database.Adapter.Entities.Extensions;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static Database.Adapter.Entities.Constants.SqlConstants.SqlSchema;

namespace Database.Adapter.Infrastructure.Configurations.MasterData;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class CardTypeConfiguration : IEntityTypeConfiguration<CardType>
{
	public void Configure(EntityTypeBuilder<CardType> builder)
	{
		builder.ToSytemVersionedTable(nameof(CardType), ENUMERATE);

		builder.HasKey(e => e.Id)
			.IsClustered(true);

		builder.HasMany(e => e.Cards)
			.WithOne(e => e.CardType)
			.HasForeignKey(e => e.CardTypeId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired(true);

		builder.HasData(GetEnDayTypes());
	}

	private static IEnumerable<CardType> GetEnDayTypes()
	{
		List<Entities.Enumerators.CardType> enumList = Entities.Enumerators.CardType.CREDIT.GetListFromEnum();
		IList<CardType> listToReturn = new List<CardType>();

		foreach (Entities.Enumerators.CardType dayType in enumList)
			listToReturn.Add(new CardType()
			{
				Id = (int)dayType,
				Name = dayType.GetEnumName(),
				Description = dayType.GetEnumDescription(),
				IsActive = true
			});

		return listToReturn;
	}
}
