using Database.Adapter.Entities.Extensions;
using Database.Adapter.Entities.MasterData;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static Database.Adapter.Entities.Constants.SqlConstants;
using Database.Adapter.Entities.Enumerators;

namespace Database.Adapter.Infrastructure.Configurations.MasterData;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class EnDayTypeConfiguration : IEntityTypeConfiguration<EnDayType>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<EnDayType> builder)
	{
		builder.ToSytemVersionedTable(nameof(EnDayType), SqlSchema.PRIVATE, SqlSchema.HISTORY);

		builder.Property(e => e.IsActive)
			.IsRequired(true)
			.HasDefaultValue(true);

		builder.HasData(GetEnDayTypes());
	}

	private static IEnumerable<EnDayType> GetEnDayTypes()
	{
		List<DayType> enumList = DayType.Absence.GetListFromEnum();
		IList<EnDayType> listToReturn = new List<EnDayType>();

		foreach (DayType dayType in enumList)
			listToReturn.Add(new EnDayType()
			{
				Id = (int)dayType,
				Name = dayType.GetEnumName(),
				Description = dayType.GetEnumDescription()
			});
		
		return listToReturn;
	}
}
