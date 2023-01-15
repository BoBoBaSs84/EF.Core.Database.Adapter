using Database.Adapter.Entities.Contexts.MasterData;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Infrastructure.Contexts;

public sealed partial class MasterDataContext
{
	/// <summary>The <see cref="Calendars"/> property.</summary>
	public DbSet<CalendarDay> Calendars { get; set; } = default!;
	/// <summary>The <see cref="DayTypes"/> property.</summary>
	public DbSet<DayType> DayTypes { get; set; } = default!;
}
