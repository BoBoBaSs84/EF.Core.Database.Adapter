using Domain.Entities.Private;
using Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static class PrivateConfiguration
{
	/// <inheritdoc/>
	internal sealed class AttendanceConfiguration : FullAuditTypeBaseConfiguration<Attendance>
	{
		public override void Configure(EntityTypeBuilder<Attendance> builder)
		{
			builder.HasIndex(e => new { e.UserId, e.CalendarDayId, e.IsDeleted })
				.IsUnique(true)
				.HasFilter($"[{nameof(Attendance.IsDeleted)}]<>(1)");

			base.Configure(builder);
		}
	}

	/// <inheritdoc/>
	internal sealed class CalendarConfiguration : IdentityTypeBaseConfiguration<CalendarDay>
	{
		/// <inheritdoc/>
		public override void Configure(EntityTypeBuilder<CalendarDay> builder)
		{
			builder.HasIndex(e => e.Date)
				.IsUnique(true);

			builder.HasIndex(e => e.Year)
				.IsUnique(false);

			builder.HasIndex(e => e.Month)
				.IsUnique(false);

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

			base.Configure(builder);
		}
	}
}
