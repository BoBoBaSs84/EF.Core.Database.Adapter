using BB84.Extensions;
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
	/// Registers the <see cref="IHomeApiClient"/> in the dependency injection container.
	/// </summary>
	/// <param name="services">The services collection to register the services in.</param>
	/// <param name="options">The options for the API client.</param>
	/// <returns>The same service collection instance, so that multiple calls can be chained.</returns>
	public static IServiceCollection RegisterHomeApiClient(this IServiceCollection services, IOptions<HomeApiOption> options)
	{
		HomeApiOption apiSettings = options.Value;

		services.TryAddTransient<AuthorizationHandler>();

		services.AddRefitClient<IHomeApiClient>()
			.ConfigureHttpClient(client => client
				.WithBaseAddress(apiSettings.BaseAddress)
				.WithTimeout(TimeSpan.FromSeconds(apiSettings.Timeout)))
			.AddHttpMessageHandler<AuthorizationHandler>();

		return services;
	}
}
