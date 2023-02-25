using DA.Infrastructure.Data;
using DA.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using static DA.Domain.Constants.Sql;

namespace DA.Infrastructure.Factories;

/// <summary>
/// The application context factory class.
/// </summary>
internal sealed class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
	/// <inheritdoc/>
	public ApplicationContext CreateDbContext(string[] args)
	{
		Configuration configuration = new();
		DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new();
		optionsBuilder.UseSqlServer(configuration.GetConnectionString(nameof(ApplicationContext)),
			options => options.MigrationsHistoryTable("Migration", Schema.PRIVATE));
#if DEBUG
		optionsBuilder.EnableSensitiveDataLogging(true);
		optionsBuilder.EnableDetailedErrors(true);
		optionsBuilder.LogTo(Console.WriteLine);
#else
		optionsBuilder.EnableSensitiveDataLogging(false);
		optionsBuilder.EnableDetailedErrors(false);
#endif
		return new(optionsBuilder.Options);
	}
}
