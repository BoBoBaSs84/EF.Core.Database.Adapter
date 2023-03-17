using Application.Interfaces.Infrastructure;
using Application.Interfaces.Infrastructure.Identity;
using Domain.Constants;
using Domain.Entities.Identity;
using Domain.Enumerators;
using Domain.Extensions;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Installer;

/// <summary>
/// Helper class for infrastructure dependency injection.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
public static class DependencyInjectionHelper
{
	/// <summary>
	/// Enriches a service collection with the infrastructure services.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <param name="configuration">The current configuration.</param>
	/// <param name="environment">The hosting environment.</param>
	/// <returns>The enriched service collection.</returns>
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
	{
		services.AddApplicationContext(configuration, environment);
		services.AddIdentityService();

		services.TryAddScoped<SaveChangesInterceptor>();
		services.TryAddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}

	/// <summary>
	/// Registers the application context.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <param name="configuration">The current configuration.</param>
	/// <param name="environment">The hosting environment.</param>
	/// <returns>The enriched service collection.</returns>
	private static IServiceCollection AddApplicationContext(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
	{
		services.AddDbContext<ApplicationContext>(options =>
		{
			options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"),
				builder =>
				{
					builder.MigrationsHistoryTable("Migration", DomainConstants.Sql.Schema.PRIVATE);
					builder.MigrationsAssembly(typeof(IInfrastructureAssemblyMarker).Assembly.FullName);
				});

			if (environment.IsDevelopment())
				options.LogTo(Console.WriteLine, LogLevel.Debug);

			if (environment.IsEnvironment(DomainConstants.Environment.Testing))
				options.LogTo(Console.WriteLine, LogLevel.Error);

			if (!environment.IsProduction())
			{
				options.EnableSensitiveDataLogging(true);
				options.EnableDetailedErrors(true);
			}
		});

		return services;
	}

	/// <summary>
	/// Registers the identity service.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	private static IServiceCollection AddIdentityService(this IServiceCollection services)
	{
		services.AddIdentity<User, Role>(options =>
		{
			options.SignIn.RequireConfirmedAccount = true;
			options.SignIn.RequireConfirmedEmail = true;
			options.Password.RequireDigit = true;
			options.Password.RequireLowercase = true;
			options.Password.RequireUppercase = true;
			options.Password.RequiredLength = 12;
			options.Lockout.MaxFailedAccessAttempts = 3;
			options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
			options.User.RequireUniqueEmail = true;
		})
			.AddEntityFrameworkStores<ApplicationContext>()
			.AddDefaultTokenProviders()
			.AddUserManager<UserService>()
			.AddRoleManager<RoleService>();

		services.AddAuthentication();
		services.AddAuthorization(options => options.AddPolicy("CanPurge",
			policy => policy.RequireRole(RoleTypes.ADMINISTRATOR.GetName())));

		services.TryAddTransient<IUserService, UserService>();
		services.TryAddTransient<IRoleService, RoleService>();

		return services;
	}
}
