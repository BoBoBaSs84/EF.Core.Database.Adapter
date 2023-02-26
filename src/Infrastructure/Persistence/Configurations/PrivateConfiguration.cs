using Domain.Entities.Private;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static class PrivateConfiguration
{
	/// <inheritdoc/>
	internal sealed class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
	{
		public void Configure(EntityTypeBuilder<Attendance> builder)
		{
			builder.ToSytemVersionedTable(nameof(Attendance));

			builder.HasKey(e => e.Id)
				.IsClustered(false);
		}
	}

	/// <inheritdoc/>
	internal sealed class CalendarConfiguration : IEntityTypeConfiguration<CalendarDay>
	{
		/// <inheritdoc/>
		public void Configure(EntityTypeBuilder<CalendarDay> builder)
		{
			builder.ToSytemVersionedTable(nameof(CalendarDay));

			builder.HasKey(e => e.Id)
				.IsClustered(false);

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

			builder.HasMany(e => e.Attendances)
				.WithOne(e => e.CalendarDay)
				.HasForeignKey(e => e.CalendarDayId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(true);
		}
	}
}
