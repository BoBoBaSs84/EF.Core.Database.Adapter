using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Attendance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static partial class AttendanceConfiguration
{
	/// <inheritdoc/>
	internal sealed class AttendanceModelConfiguration : AuditedConfiguration<AttendanceModel>
	{
		public override void Configure(EntityTypeBuilder<AttendanceModel> builder)
		{
			builder.ToHistoryTable("Attendance", SqlSchema.Attendance, SqlSchema.History);

			builder.HasIndex(i => new { i.UserId, i.Date })
				.IsClustered(false)
				.IsUnique();

			builder.Property(p => p.Date)
				.HasDateColumnType();

			builder.Property(p => p.StartTime)
				.HasTimeColumnType(0);

			builder.Property(p => p.EndTime)
				.HasTimeColumnType(0);

			builder.Property(p => p.BreakTime)
				.HasTimeColumnType(0);

			base.Configure(builder);
		}
	}
}
