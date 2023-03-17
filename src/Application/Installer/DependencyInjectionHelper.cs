using Application.Interfaces.Application;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Application.Installer;

/// <summary>
/// Helper class for application dependency injection.
/// </summary>
public static class DependencyInjectionHelper
{
	/// <summary>
	/// Enriches a service collection with the application services.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <param name="configuration">The current configuration.</param>
	/// <param name="environment">The hosting environment.</param>
	/// <returns>The enriched service collection.</returns>
	public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
	{
		services.TryAddScoped<IAuthenticationService, AuthenticationService>();
		return services;
	}
}
