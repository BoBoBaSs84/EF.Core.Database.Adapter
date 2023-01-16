using Database.Adapter.Entities.Contexts.Timekeeping;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Infrastructure.Contexts;

public sealed partial class MasterDataContext
{
	/// <summary>The <see cref="Attendances"/> property.</summary>
	public DbSet<Attendance> Attendances { get; set; } = default!;
}
