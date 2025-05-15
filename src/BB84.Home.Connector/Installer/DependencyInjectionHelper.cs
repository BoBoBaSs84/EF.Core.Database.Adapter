using BB84.Home.Connector.Abstractions;
using BB84.Home.Connector.Handlers;
using BB84.Home.Connector.Options;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

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
	/// <param name="options">The options for the API client.</param>
	/// <returns>The same service collection instance, so that multiple calls can be chained.</returns>
	public static IServiceCollection RegisterConnector(this IServiceCollection services, IOptions<ApiSettings> options)
	{
		var apiSettings = options.Value;

		services.AddRefitClient<IBB84HomeAPI>()
			.ConfigureHttpClient(c =>
			{
				c.BaseAddress = new Uri(apiSettings.BaseAddress);
				c.Timeout = TimeSpan.FromSeconds(apiSettings.Timeout);
			})
			.AddHttpMessageHandler<AuthorizationHandler>();

		services.TryAddTransient<AuthorizationHandler>();

		return services;
	}
}
