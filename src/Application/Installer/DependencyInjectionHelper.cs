using Application.Extensions;

using Microsoft.Extensions.DependencyInjection;

namespace Application.Installer;

/// <summary>
/// Helper class for application dependency injection.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, dependency injection.")]
public static class DependencyInjectionHelper
{
	/// <summary>
	/// Enriches a service collection with the application services.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
	{
		services.ConfigureAutoMapper();
		services.ConfigureScopedServices();

		return services;
	}
}
