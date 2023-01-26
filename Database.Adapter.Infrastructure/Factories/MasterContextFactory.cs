﻿using Database.Adapter.Infrastructure.Configurations;
using Database.Adapter.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using static Database.Adapter.Entities.Constants.Sql;

namespace Database.Adapter.Infrastructure.Factories;

/// <summary>
/// The master context factory class.
/// </summary>
internal sealed class MasterContextFactory : IDesignTimeDbContextFactory<MasterContext>
{
	/// <inheritdoc/>
	public MasterContext CreateDbContext(string[] args) => new(DbContextOptions);

	/// <summary>
	/// The <see cref="DbContextOptions"/> property provides fast access to the
	/// options of the <see cref="MasterContext"/>.
	/// </summary>
	public static DbContextOptions<MasterContext> DbContextOptions
	{
		get
		{
			Configuration configuration = new();
			DbContextOptionsBuilder<MasterContext> optionsBuilder = new();
			optionsBuilder.UseSqlServer(configuration.GetConnectionString(nameof(MasterContext)),
				options => options.MigrationsHistoryTable(nameof(MasterContext), Schema.MIGRATION));
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
