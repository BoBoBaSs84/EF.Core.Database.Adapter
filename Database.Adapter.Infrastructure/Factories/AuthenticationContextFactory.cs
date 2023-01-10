using Database.Adapter.Infrastructure.Configurations;
using Database.Adapter.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database.Adapter.Infrastructure.Factories;

/// <inheritdoc/>
public sealed class AuthenticationContextFactory : IDesignTimeDbContextFactory<AuthenticationContext>
{
	/// <inheritdoc/>
	public AuthenticationContext CreateDbContext(string[] args) => new(DbContextOptions);

	/// <summary>
	/// The <see cref="DbContextOptions"/> property provides fast access to the
	/// options of the <see cref="AuthenticationContext"/>.
	/// </summary>
	public static DbContextOptions<AuthenticationContext> DbContextOptions
	{
		get
		{
			Configuration configuration = new();
			DbContextOptionsBuilder<AuthenticationContext> optionsBuilder = new();
			optionsBuilder.UseSqlServer(configuration.GetConnectionString(nameof(AuthenticationContext)));
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
