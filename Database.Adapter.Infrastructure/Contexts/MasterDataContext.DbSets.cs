using Database.Adapter.Entities.MasterData;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Infrastructure.Contexts;

public partial class MasterDataContext
{
	/// <summary>The <see cref="Calendars"/> property.</summary>
	public DbSet<Calendar> Calendars { get; set; }
}
