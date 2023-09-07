using Domain.Models.Common;

using Infrastructure.Extensions;
using Infrastructure.Persistence.Configurations.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal static partial class CommonConfiguration
{
	/// <inheritdoc/>
	internal sealed class CalendarConfiguration : IdentityBaseConfiguration<CalendarModel>
	{
		/// <inheritdoc/>
		public override void Configure(EntityTypeBuilder<CalendarModel> builder)
		{
			builder.ToSytemVersionedTable(SqlSchema.Common, "Calendar");

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
				.WithOne(e => e.Calendar)
				.HasForeignKey(e => e.CalendarId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			base.Configure(builder);
		}
	}
}
