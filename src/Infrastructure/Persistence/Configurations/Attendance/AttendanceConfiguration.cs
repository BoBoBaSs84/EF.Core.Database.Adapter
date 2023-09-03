using Domain.Models.Attendance;

using Infrastructure.Extensions;
using Infrastructure.Persistence.Configurations.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations.Attendance;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class AttendanceConfiguration : IdentityBaseConfiguration<AttendanceModel>
{
	public override void Configure(EntityTypeBuilder<AttendanceModel> builder)
	{
		builder.ToSytemVersionedTable(SqlSchema.Attendance, "Attendance");

		builder.HasIndex(e => new { e.UserId, e.CalendarDayId })
			.IsClustered(false)
			.IsUnique(true);

		base.Configure(builder);
	}
}
