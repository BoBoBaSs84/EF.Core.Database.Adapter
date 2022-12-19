using Database.Adapter.Entities.Extensions;
using Database.Adapter.Entities.MasterData;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Infrastructure.Configurations.MasterData;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class DayTypeConfiguration : IEntityTypeConfiguration<DayType>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<DayType> builder)
	{
		builder.ToSytemVersionedTable(nameof(DayType), SqlSchema.ENUMERATOR);

		builder.HasData(GetEnDayTypes());
	}

	private static IEnumerable<DayType> GetEnDayTypes()
	{
		List<Entities.Enumerators.DayType> enumList = Entities.Enumerators.DayType.Absence.GetListFromEnum();
		IList<DayType> listToReturn = new List<DayType>();

		foreach (Entities.Enumerators.DayType dayType in enumList)
			listToReturn.Add(new DayType()
			{
				Id = (int)dayType,
				Name = dayType.GetEnumName(),
				Description = dayType.GetEnumDescription(),
				IsActive = true
			});
		
		return listToReturn;
	}
}
