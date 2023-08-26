using Microsoft.Extensions.DependencyInjection;

using Presentation.Extensions;

namespace Presentation.Installer;

/// <summary>
/// Helper class for presentation dependency injection.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, dependency injection.")]
public static class DependencyInjectionHelper
{
	/// <summary>
	/// Enriches a service collection with the presentation services.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	public static IServiceCollection ConfigurePresentationServices(this IServiceCollection services)
	{
		services.ConfigureSingletonServices();
		services.ConfigureApiVersioning();
		services.ConfigureApiControllers();

		return services;
	}
}
