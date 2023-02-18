using DA.Repositories.Manager;
using DA.Repositories.Manager.Interfaces;
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
	/// <returns>An enriched service colletcion.</returns>
	public static IServiceCollection GetRepositoryManagerService(this IServiceCollection services) =>
		services.AddScoped<IRepositoryManager, RepositoryManager>();
}
