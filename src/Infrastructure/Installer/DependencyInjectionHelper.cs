using Infrastructure.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
	public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
	{
		services.AddMicrosoftLogger();

		services.AddRepositoryContext(configuration, environment);
		services.AddIdentityService();
		services.ConfigureJWT(configuration);

		services.ConfigureScopedServices();
		services.ConfigureTransientServices();

		return services;
	}
}
