using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Attendance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static partial class AttendanceConfiguration
{
	/// <inheritdoc/>
	internal sealed class AttendanceModelConfiguration : IdentityConfiguration<AttendanceModel>
	{
		public override void Configure(EntityTypeBuilder<AttendanceModel> builder)
		{
			builder.ToHistoryTable("Attendance", SqlSchema.Attendance, SqlSchema.History);

			builder.HasIndex(e => new { e.UserId, e.CalendarId })
				.IsClustered(false)
				.IsUnique();

			base.Configure(builder);
		}
	}
}
