using Database.Adapter.Entities.Contexts.Application.MasterData;
using Database.Adapter.Entities.Contexts.Application.Timekeeping;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Infrastructure.Contexts;

public sealed partial class ApplicationContext
{
	/// <summary>The <see cref="CalendarDays"/> property.</summary>
	public DbSet<CalendarDay> CalendarDays { get; set; } = default!;
	/// <summary>The <see cref="DayTypes"/> property.</summary>
	public DbSet<DayType> DayTypes { get; set; } = default!;
	/// <summary>The <see cref="Attendances"/> property.</summary>
	public DbSet<Attendance> Attendances { get; set; } = default!;
}
