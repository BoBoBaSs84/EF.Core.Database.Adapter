﻿using Database.Adapter.Infrastructure.Configurations;
using Database.Adapter.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database.Adapter.Infrastructure.Factories;

/// <inheritdoc/>
public sealed class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
	/// <inheritdoc/>
	public ApplicationContext CreateDbContext(string[] args) => new(DbContextOptions);

	/// <summary>
	/// The <see cref="DbContextOptions"/> property provides fast access to the
	/// options of the <see cref="ApplicationContext"/>.
	/// </summary>
	public static DbContextOptions<ApplicationContext> DbContextOptions
	{
		get
		{
			Configuration configuration = new();
			DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new();
			optionsBuilder.UseSqlServer(configuration.GetConnectionString(nameof(ApplicationContext)));
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
