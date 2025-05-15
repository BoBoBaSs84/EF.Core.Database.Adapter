using BB84.Extensions;
using BB84.Home.Connector.Abstractions;
using BB84.Home.Connector.Handlers;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Refit;

namespace BB84.Home.Connector.Installer;

/// <summary>
/// This class is used to register the services in the dependency injection container.
/// </summary>
public static class DependencyInjectionHelper
{
	/// <summary>
	/// Registers the connector services in the dependency injection container.
	/// </summary>
	/// <param name="services">The services collection to register the services in.</param>
	/// <param name="baseAddress">The base address of the API.</param>
	/// <returns>The same service collection instance, so that multiple calls can be chained.</returns>
	public static IServiceCollection RegisterConnector(this IServiceCollection services, string baseAddress)
	{
		services.TryAddTransient<AuthorizationHandler>();

		services.AddRefitClient<IBB84HomeAPI>()
			.ConfigureHttpClient(c => c.WithBaseAdress(baseAddress)
				.WithMediaType("application/json")
				.WithTimeout(TimeSpan.FromSeconds(30))
				)
			.AddHttpMessageHandler<AuthorizationHandler>();

		return services;
	}
}
