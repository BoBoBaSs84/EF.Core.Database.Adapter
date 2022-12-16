using Database.Adapter.Entities.MasterData;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Infrastructure.Contexts;

public sealed partial class MasterDataContext
{
	/// <summary>The <see cref="Calendars"/> property.</summary>
	public DbSet<Calendar> Calendars { get; set; }
	/// <summary>The <see cref="DayTypes"/> property.</summary>
	public DbSet<EnDayType> DayTypes { get; set; }
}
