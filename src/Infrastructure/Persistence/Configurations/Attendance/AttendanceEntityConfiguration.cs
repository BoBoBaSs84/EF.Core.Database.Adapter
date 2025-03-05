using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;
using BB84.Home.Domain.Entities.Attendance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static BB84.Home.Infrastructure.Common.InfrastructureConstants;

namespace BB84.Home.Infrastructure.Persistence.Configurations.Attendance;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class AttendanceEntityConfiguration : AuditedConfiguration<AttendanceEntity>
{
	public override void Configure(EntityTypeBuilder<AttendanceEntity> builder)
	{
		builder.ToHistoryTable("Attendance", SqlSchema.Attendance, SqlSchema.History);

		builder.HasIndex(i => new { i.UserId, i.Date })
			.IsClustered(false)
			.IsUnique();

		builder.Property(p => p.Date)
			.IsDateColumn();

		builder.Property(p => p.StartTime)
			.IsTimeColumn(0);

		builder.Property(p => p.EndTime)
			.IsTimeColumn(0);

		builder.Property(p => p.BreakTime)
			.IsTimeColumn(0);

		base.Configure(builder);
	}
}
