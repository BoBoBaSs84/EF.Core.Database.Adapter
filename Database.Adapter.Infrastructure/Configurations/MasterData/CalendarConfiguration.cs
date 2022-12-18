using Database.Adapter.Entities.MasterData;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Infrastructure.Configurations.MasterData;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class CalendarConfiguration : IEntityTypeConfiguration<CalendarDay>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<CalendarDay> builder)
	{
		builder.ToSytemVersionedTable(nameof(CalendarDay));

		builder.Property(e => e.Day)
			.HasComputedColumnSql("(datepart(day,[Date]))", true);

		builder.Property(e => e.DayOfYear)
			.HasComputedColumnSql("(datepart(dayofyear,[Date]))", true);

		builder.Property(e => e.EndOfMonth)
			.HasComputedColumnSql("(eomonth([Date]))", true);

		builder.Property(e => e.IsoWeek)
			.HasComputedColumnSql("(datepart(iso_week,[Date]))", true);

		builder.Property(e => e.Month)
			.HasComputedColumnSql("(datepart(month,[Date]))", true);

		builder.Property(e => e.MonthName)
			.HasComputedColumnSql("(datename(month,[Date]))", false);

		builder.Property(e => e.Week)
			.HasComputedColumnSql("(datepart(week,[Date]))", false);

		builder.Property(e => e.WeekDay)
			.HasComputedColumnSql("(datepart(weekday,[Date]))", false);

		builder.Property(e => e.WeekDayName)
			.HasComputedColumnSql("(datename(weekday,[Date]))", false);

		builder.Property(e => e.Year)
			.HasComputedColumnSql("(datepart(year,[Date]))", true);

		builder.HasData(GetCalendarDays());
	}

	private static IEnumerable<CalendarDay> GetCalendarDays()
	{
		DateTime dateStart = new(1950, 1, 1), dateEnd = new(2050, 12, 31);
		IList<CalendarDay> calendarDays = new List<CalendarDay>();
		while (dateStart <= dateEnd)
		{
			if (dateStart.DayOfWeek.Equals(DayOfWeek.Sunday) || dateStart.DayOfWeek.Equals(DayOfWeek.Saturday))

			calendarDays.Add(new CalendarDay()
			{
				Id = Guid.NewGuid(),
				Date = dateStart
			});
			dateStart = dateStart.AddDays(1);
		}
		return calendarDays;
	}
}
