using Database.Adapter.Infrastructure.Configurations;
using Database.Adapter.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database.Adapter.Infrastructure.Factories;

/// <inheritdoc/>
public sealed class TimekeepingContextFactory : IDesignTimeDbContextFactory<TimekeepingContext>
{
	/// <inheritdoc/>
	public TimekeepingContext CreateDbContext(string[] args) => new(DbContextOptions);

	/// <summary>
	/// The <see cref="DbContextOptions"/> property provides fast access to the
	/// options of the <see cref="TimekeepingContext"/>.
	/// </summary>
	public static DbContextOptions<TimekeepingContext> DbContextOptions
	{
		get
		{
			Configuration configuration = new();
			DbContextOptionsBuilder<TimekeepingContext> optionsBuilder = new();
			string connectionString = configuration.GetConnectionString(nameof(TimekeepingContext));
			optionsBuilder.UseSqlServer(connectionString);
#if DEBUG
			optionsBuilder.UseLoggerFactory(Statics.LoggerFactory);
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
