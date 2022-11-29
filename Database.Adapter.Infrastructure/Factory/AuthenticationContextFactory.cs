using Database.Adapter.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database.Adapter.Infrastructure.Factory;

/// <inheritdoc/>
internal sealed class AuthenticationContextFactory : IDesignTimeDbContextFactory<AuthenticationContext>
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
			// TODO: No connection string in code!
			DbContextOptionsBuilder<AuthenticationContext> optionsBuilder = new();
#if DEBUG
			optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Authentication;Integrated Security=True;");
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
