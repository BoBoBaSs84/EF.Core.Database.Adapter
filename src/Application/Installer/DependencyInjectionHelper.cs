using Application.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Installer;

/// <summary>
/// The dependency injection helper class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, dependency injection.")]
public static class DependencyInjectionHelper
{
	/// <summary>
	/// Registers the required application services to the provided <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <param name="configuration">The current application configuration.</param>
	/// <returns>The enriched service collection.</returns>
	public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.RegisterApplicationOptions(configuration)
			.RegisterAutoMapper()
			.RegisterScopedServices()
			.RegisterSingletonServices();

		return services;
	}
}
