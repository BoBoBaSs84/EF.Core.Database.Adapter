using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Infrastructure.Configurations.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database.Adapter.Infrastructure.Factory;

/// <inheritdoc/>
public sealed class MasterDataContextFactory : IDesignTimeDbContextFactory<MasterDataContext>
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
			Configuration configuration = new();
			DbContextOptionsBuilder<MasterDataContext> optionsBuilder = new();
			optionsBuilder.UseSqlServer(configuration.GetConnectionString(nameof(MasterDataContext)));
#if DEBUG
			optionsBuilder.EnableSensitiveDataLogging(true);
			optionsBuilder.EnableDetailedErrors(true);
#else
		optionsBuilder.EnableSensitiveDataLogging(false);
		optionsBuilder.EnableDetailedErrors(false);
#endif
			return optionsBuilder.Options;
		}
	}
}
