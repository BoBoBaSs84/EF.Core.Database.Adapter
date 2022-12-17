using Database.Adapter.Entities.MasterData;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Infrastructure.Configurations.MasterData;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class CalendarConfiguration : IEntityTypeConfiguration<Calendar>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<Calendar> builder)
	{
		builder.ToSytemVersionedTable(nameof(Calendar), SqlSchema.PRIVATE, SqlSchema.HISTORY);

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
	}
}
