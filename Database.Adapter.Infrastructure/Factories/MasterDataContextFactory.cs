using Database.Adapter.Infrastructure.Configurations;
using Database.Adapter.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database.Adapter.Infrastructure.Factories;

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
			string connectionString = configuration.GetConnectionString(nameof(MasterDataContext));
			optionsBuilder.UseSqlServer(connectionString);
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
