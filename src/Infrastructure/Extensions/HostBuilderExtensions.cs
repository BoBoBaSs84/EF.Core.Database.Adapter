using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Environment = Domain.Common.Constants.Environment;

namespace Infrastructure.Extensions;

/// <summary>
/// The host builder extensions class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
public static class HostBuilderExtensions
{
	/// <summary>
	/// Configures the application settings for the host builder.
	/// </summary>
	/// <param name="hostBuilder">The host builder to work with.</param>
	/// <param name="enviroment">The enviroment to load additionally.</param>
	/// <returns>The enriched <paramref name="hostBuilder"/>.</returns>
	public static IHostBuilder ConfigureAppSettings(this IHostBuilder hostBuilder, string? enviroment = null)
	{
		enviroment ??= Environment.Development;

		hostBuilder.ConfigureAppConfiguration((context, builder) =>
		{
			builder.AddJsonFile("appsettings.json", false, true);
			builder.AddJsonFile($"appsettings.{enviroment}.json", true, true);
			builder.AddEnvironmentVariables();
		});

		return hostBuilder;
	}
}
