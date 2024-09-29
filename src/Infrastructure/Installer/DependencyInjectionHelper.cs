using Infrastructure.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Installer;

/// <summary>
/// The dependency injection helper class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
public static class DependencyInjectionHelper
{
	/// <summary>
	/// Registers the required infrastructure services to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <param name="configuration">The current configuration to use.</param>
	/// <param name="environment">The current hosting environment to use.</param>
	/// <returns>The enriched service collection.</returns>
	public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
	{
		services.RegisterScopedServices()
			.RegisterSingletonServices()
			.RegisterRepositoryContext(configuration, environment)
			.RegisterIdentityService()
			.RegisterJwtBearerConfiguration(configuration);

		return services;
	}
}
