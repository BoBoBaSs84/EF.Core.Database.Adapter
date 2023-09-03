using Domain.Models.Attendance;

using Infrastructure.Extensions;
using Infrastructure.Persistence.Configurations.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static class AttendanceConfiguration
{
	/// <inheritdoc/>
	internal sealed class AttendanceModelConfiguration : IdentityBaseConfiguration<AttendanceModel>
	{
		public override void Configure(EntityTypeBuilder<AttendanceModel> builder)
		{
			builder.ToSytemVersionedTable(SqlSchema.Attendance, "Attendance");

			builder.HasIndex(e => new { e.UserId, e.CalendarDayId })
				.IsClustered(false)
				.IsUnique();

			base.Configure(builder);
		}
	}

	internal sealed class AttendanceSettingsModelConfiguration : IdentityBaseConfiguration<AttendanceSettingsModel>
	{
		public override void Configure(EntityTypeBuilder<AttendanceSettingsModel> builder)
		{
			builder.ToSytemVersionedTable(SqlSchema.Attendance, "Settings");

			builder.HasIndex(e => e.UserId)
				.IsClustered(false)
				.IsUnique();

			base.Configure(builder);
		}
	}
}
