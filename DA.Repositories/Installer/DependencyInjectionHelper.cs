using DA.Repositories.Manager;
using DA.Repositories.Manager.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DA.Repositories.Installer;

/// <summary>
/// Helper class for repository dependency injection.
/// </summary>
public static class DependencyInjectionHelper
{
	/// <summary>
	/// Enriches a service collection with the repository services.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection</returns>
	public static IServiceCollection GetRepositoryService(this IServiceCollection services)
	{
		services.TryAddScoped<IRepositoryManager, RepositoryManager>();
		return services;
	}
}
