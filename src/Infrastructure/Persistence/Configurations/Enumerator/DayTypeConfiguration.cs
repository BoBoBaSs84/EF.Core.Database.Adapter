using Domain.Entities.Enumerator;
using Domain.Extensions;

using Infrastructure.Extensions;
using Infrastructure.Persistence.Configurations.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EDT = Domain.Enumerators.DayTypes;
using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations.Enumerator;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class DayTypeConfiguration : EnumeratorTypeBaseConfiguration<DayType>
{
	/// <inheritdoc/>
	public override void Configure(EntityTypeBuilder<DayType> builder)
	{
		builder.ToSytemVersionedTable(SqlSchema.ENUMERATOR);

		builder.HasMany(e => e.CalendarDays)
			.WithOne(e => e.DayType)
			.HasForeignKey(e => e.DayTypeId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired();

		builder.HasMany(e => e.Attendances)
			.WithOne(e => e.DayType)
			.HasForeignKey(e => e.DayTypeId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired();

		builder.HasData(GetDayTypeData());

		base.Configure(builder);
	}

	private static IEnumerable<DayType> GetDayTypeData()
	{
		IList<EDT> enumList = EDT.ABSENCE.GetListFromEnum();
		IList<DayType> listToReturn = new List<DayType>();

		foreach (EDT dayType in enumList)
			listToReturn.Add(new DayType()
			{
				Id = (int)dayType,
				Name = dayType.GetName(),
				Description = dayType.GetDescription()
			});

		return listToReturn;
	}
}