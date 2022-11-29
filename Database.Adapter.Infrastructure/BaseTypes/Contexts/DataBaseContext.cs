using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Infrastructure.BaseTypes.Contexts;

// TODO: maybe this willl help
public abstract class DataBaseContext : DbContext
{
	protected DataBaseContext()
	{
	}

	protected DataBaseContext(DbContextOptions options) : base(options)
	{
	}
}
