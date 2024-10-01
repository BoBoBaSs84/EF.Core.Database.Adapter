using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Attendance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlDataType = Domain.Common.Constants.Sql.DataType;
using SqlSchema = Domain.Common.Constants.Sql.Schema;

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
				.HasColumnType(SqlDataType.DATE);

			builder.Property(p => p.StartTime)
				.HasColumnType(SqlDataType.TIME0);

			builder.Property(p => p.EndTime)
				.HasColumnType(SqlDataType.TIME0);

			builder.Property(p => p.BreakTime)
				.HasColumnType(SqlDataType.TIME0);

			base.Configure(builder);
		}
	}
}
