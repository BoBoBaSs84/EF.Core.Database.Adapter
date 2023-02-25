using DA.Infrastructure.Configurations;
using DA.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using static DA.Models.Constants.Sql;

namespace DA.Infrastructure.Factories;

/// <summary>
/// The application context factory class.
/// </summary>
internal sealed class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
	/// <inheritdoc/>
	public ApplicationContext CreateDbContext(string[] args) => new(Options);

	/// <summary>
	/// The <see cref="Options"/> property provides fast access to the
	/// context options of the <see cref="ApplicationContext"/>.
	/// </summary>
	public static DbContextOptions<ApplicationContext> Options => OptionsBuilder.Options;

	/// <summary>
	/// The <see cref="OptionsBuilder"/> property provides fast access to the
	/// context option builder of the <see cref="ApplicationContext"/>.
	/// </summary>
	public static DbContextOptionsBuilder<ApplicationContext> OptionsBuilder
	{
		get
		{
			Configuration configuration = new();
			DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new();
			optionsBuilder.UseSqlServer(configuration.GetConnectionString(nameof(ApplicationContext)),
				options => options.MigrationsHistoryTable($"{nameof(ApplicationContext)}{Schema.MIGRATION}", Schema.PRIVATE));
#if DEBUG
			optionsBuilder.EnableSensitiveDataLogging(true);
			optionsBuilder.EnableDetailedErrors(true);
			optionsBuilder.LogTo(Console.WriteLine);
#else
			optionsBuilder.EnableSensitiveDataLogging(false);
		optionsBuilder.EnableDetailedErrors(false);
#endif
			return optionsBuilder;
		}
	}
}
