using DA.Infrastructure.Contexts;
using DA.Models.Contexts.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DA.Infrastructure.Installer;

/// <summary>
/// Helper class for infrastructure dependency injection.
/// </summary>
public static class DependencyInjectionHelper
{
	/// <summary>
	/// Enriches a service collection with the infrastructure services.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	public static IServiceCollection GetInfrastructureServices(this IServiceCollection services)
	{
		services.AddApplicationContext();
		services.AddIdentityService();
		return services;
	}

	/// <summary>
	/// Registers the application context.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	private static void AddApplicationContext(this IServiceCollection services) =>
		services.AddDbContext<ApplicationContext>();

	/// <summary>
	/// Registers the identity service.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>	
	private static void AddIdentityService(this IServiceCollection services) =>
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
		.AddDefaultTokenProviders();
}
