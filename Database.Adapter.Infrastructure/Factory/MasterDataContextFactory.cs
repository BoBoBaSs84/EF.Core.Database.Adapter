using Database.Adapter.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database.Adapter.Infrastructure.Factory;

/// <inheritdoc/>
internal sealed class MasterDataContextFactory : IDesignTimeDbContextFactory<MasterDataContext>
{
	/// <inheritdoc/>
	public MasterDataContext CreateDbContext(string[] args) => new(DbContextOptions);

	/// <summary>
	/// The <see cref="DbContextOptions"/> property provides fast access to the
	/// options of the <see cref="MasterDataContext"/>.
	/// </summary>
	public static DbContextOptions<MasterDataContext> DbContextOptions
	{
		get
		{
			DbContextOptionsBuilder<MasterDataContext> optionsBuilder = new();
			// TODO: No connection string in code!
#if DEBUG
			optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MasterData;Integrated Security=True;");
			optionsBuilder.EnableSensitiveDataLogging(true);
			optionsBuilder.EnableDetailedErrors(true);
#else
		optionsBuilder.UseSqlServer("");
		optionsBuilder.EnableSensitiveDataLogging(false);
		optionsBuilder.EnableDetailedErrors(false);
#endif
			return optionsBuilder.Options;
		}
	}
}
