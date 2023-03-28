using Domain.Entities.Private;
using Infrastructure.Extensions;
using Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Private;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class AttendanceConfiguration : IdentityTypeBaseConfiguration<Attendance>
{
	public override void Configure(EntityTypeBuilder<Attendance> builder)
	{
		builder.ToSytemVersionedTable();

		builder.HasIndex(e => new { e.UserId, e.CalendarDayId })
			.IsClustered(false)
			.IsUnique(true);

		base.Configure(builder);
	}
}
