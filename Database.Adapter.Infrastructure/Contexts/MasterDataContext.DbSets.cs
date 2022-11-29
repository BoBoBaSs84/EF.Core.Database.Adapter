using Database.Adapter.Entities.MasterData;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Infrastructure.Contexts;

public partial class MasterDataContext
{
	public DbSet<Calendar> Calendars { get; set; }
}
