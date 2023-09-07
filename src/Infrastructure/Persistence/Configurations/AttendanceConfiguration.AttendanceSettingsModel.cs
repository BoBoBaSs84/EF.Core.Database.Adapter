using Domain.Models.Attendance;

using Infrastructure.Extensions;
using Infrastructure.Persistence.Configurations.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static partial class AttendanceConfiguration
{
	/// <inheritdoc/>
	internal sealed class AttendanceSettingsModelConfiguration : IdentityBaseConfiguration<AttendanceSettingsModel>
	{
		public override void Configure(EntityTypeBuilder<AttendanceSettingsModel> builder)
		{
			builder.ToVersionedTable(SqlSchema.Attendance, "Settings");

			builder.HasIndex(e => e.UserId)
				.IsClustered(false)
				.IsUnique();

			base.Configure(builder);
		}
	}
}
