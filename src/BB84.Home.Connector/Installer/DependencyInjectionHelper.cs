using BB84.Extensions;

using Microsoft.Extensions.DependencyInjection;

using Refit;

namespace BB84.Home.Connector.Installer;

/// <summary>
/// This class is used to register the services in the dependency injection container.
/// </summary>
public static class DependencyInjectionHelper
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="services"></param>
	/// <param name="baseAddress"></param>
	/// <returns></returns>
	public static IServiceCollection RegisterConnector(this IServiceCollection services, string baseAddress)
	{
		services.AddRefitClient<IBB84HomeAPI>().ConfigureHttpClient(
			client =>
			{
				client.WithBaseAdress(baseAddress);
				client.WithMediaType("application/json");
				client.WithTimeout(TimeSpan.FromSeconds(30));
			});

		return services;
	}
}
