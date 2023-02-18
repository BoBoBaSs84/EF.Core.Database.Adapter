using DA.Infrastructure.Contexts;
using DA.Models.Contexts.Authentication;
using DA.Repositories.Manager;
using DA.Repositories.Manager.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DA.Repositories.Installer;

/// <summary>
/// Helper class for repository dependency injection.
/// </summary>
public static class DependencyInjectionHelper
{
	/// <summary>
	/// Registers the repository manager.
	/// </summary>
	/// <param name="services">The service collection.</param>
	public static void GetRepositoryManager(this IServiceCollection services) =>
		services.AddScoped<IRepositoryManager, RepositoryManager>();

	/// <summary>
	/// Registers the identity service.
	/// </summary>
	/// <param name="services">The service collection.</param>
	public static void GetIdentityService(this IServiceCollection services) =>
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
