using Database.Adapter.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database.Adapter.Infrastructure.Factory;

/// <inheritdoc/>
internal class MasterDataContextFactory : IDesignTimeDbContextFactory<MasterDataContext>
{
	/// <inheritdoc/>
	public MasterDataContext CreateDbContext(string[] args)
	{
		// TODO: No connection string in code!
		DbContextOptionsBuilder<MasterDataContext> optionsBuilder = new();
#if DEBUG
		optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MasterData;Integrated Security=True;");
		optionsBuilder.EnableSensitiveDataLogging(true);
		optionsBuilder.EnableDetailedErrors(true);
#else
		optionsBuilder.UseSqlServer("");
		optionsBuilder.EnableSensitiveDataLogging(false);
		optionsBuilder.EnableDetailedErrors(false);
#endif
		return new MasterDataContext(optionsBuilder.Options);
	}
}
