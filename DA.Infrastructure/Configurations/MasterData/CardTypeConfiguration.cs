using DA.Domain.Extensions;
using DA.Domain.Models.MasterData;
using DA.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static DA.Domain.Constants.Sql.Schema;

namespace DA.Infrastructure.Configurations.MasterData;

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
		List<Domain.Enumerators.CardType> enumList = Domain.Enumerators.CardType.CREDIT.GetListFromEnum();
		IList<CardType> listToReturn = new List<CardType>();

		foreach (Domain.Enumerators.CardType dayType in enumList)
			listToReturn.Add(new CardType()
			{
				Id = (int)dayType,
				Name = dayType.GetName(),
				Description = dayType.GetDescription(),
				IsActive = true
			});

		return listToReturn;
	}
}
