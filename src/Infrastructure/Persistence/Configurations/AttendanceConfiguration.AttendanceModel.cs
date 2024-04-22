using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;

using Domain.Models.Attendance;

using Infrastructure.Extensions;

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
			builder.ToVersionedTable(SqlSchema.Attendance, "Attendance");

			builder.HasIndex(e => new { e.UserId, e.CalendarId })
				.IsClustered(false)
				.IsUnique();

			base.Configure(builder);
		}
	}
}
