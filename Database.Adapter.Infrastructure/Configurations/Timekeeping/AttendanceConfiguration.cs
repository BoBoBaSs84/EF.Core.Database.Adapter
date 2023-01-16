using Database.Adapter.Entities.Contexts.Timekeeping;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Infrastructure.Configurations.Timekeeping;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
	public void Configure(EntityTypeBuilder<Attendance> builder)
	{
		builder.ToSytemVersionedTable(nameof(Attendance));

		builder.HasKey(e => e.Id)
			.IsClustered(false);
	}
}
