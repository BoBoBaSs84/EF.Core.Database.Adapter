﻿using Database.Adapter.Entities.Contexts.MasterData;
using Database.Adapter.Entities.Extensions;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static Database.Adapter.Entities.Constants.Sql.Schema;

namespace Database.Adapter.Infrastructure.Configurations.MasterData;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class DayTypeConfiguration : IEntityTypeConfiguration<DayType>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<DayType> builder)
	{
		builder.ToSytemVersionedTable(nameof(DayType), ENUMERATE);

		builder.HasKey(e => e.Id)
			.IsClustered(false);

		builder.HasMany(e => e.CalendarDays)
			.WithOne(e => e.DayType)
			.HasForeignKey(e => e.DayTypeId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired(true);

		builder.HasMany(e => e.Attendances)
			.WithOne(e => e.DayType)
			.HasForeignKey(e => e.DayTypeId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired(true);

		builder.HasData(GetEnDayTypes());
	}

	private static IEnumerable<DayType> GetEnDayTypes()
	{
		List<Entities.Enumerators.DayType> enumList = Entities.Enumerators.DayType.ABSENCE.GetListFromEnum();
		IList<DayType> listToReturn = new List<DayType>();

		foreach (Entities.Enumerators.DayType dayType in enumList)
			listToReturn.Add(new DayType()
			{
				Id = (int)dayType,
				Name = dayType.GetName(),
				Description = dayType.GetDescription(),
				IsActive = true
			});

		return listToReturn;
	}
}
