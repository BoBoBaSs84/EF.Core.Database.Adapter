using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using static Domain.Constants.Environment;

namespace Infrastructure.Extensions;

/// <summary>
/// The host builder extensions class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
public static class HostBuilderExtensions
{
	/// <summary>
	/// Applies additional configuration to the host.
	/// </summary>
	/// <param name="host">The host builder to work with.</param>
	/// <param name="enviroment">The enviroment to load additionally.</param>
	/// <returns>The enriched <paramref name="host"/>.</returns>
	public static IHostBuilder ConfigureAppSettings(this IHostBuilder host, string enviroment = Development)
	{
		host.ConfigureAppConfiguration((context, builder) =>
		{
			builder.AddJsonFile("appsettings.json", false, true);
			builder.AddJsonFile($"appsettings.{enviroment}.json", true, true);
			builder.AddEnvironmentVariables();
		});

		return host;
	}
}
