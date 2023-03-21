using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Common;
using HHC = Presentation.Constants.PresentationConstants.HttpHeaders;

namespace Presentation.Installer;

/// <summary>
/// Helper class for presentation dependency injection.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
public static class DependencyInjectionHelper
{
	/// <summary>
	/// Enriches a service collection with the presentation services.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	public static IServiceCollection ConfigurePresentationServices(this IServiceCollection services)
	{
		services.ConfigureApiVersioning();
		services.ConfigureApiControllers();
		
		return services;
	}

	/// <summary>
	/// Enriches a service collection with the presentation controllers.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	private static IServiceCollection ConfigureApiControllers(this IServiceCollection services)
	{
		services.AddControllers()
			.AddApplicationPart(typeof(IPresentationAssemblyMarker).Assembly);
		
		return services;
	}

	/// <summary>
	/// Enriches a service collection with the api versioning.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	private static IServiceCollection ConfigureApiVersioning(this IServiceCollection services)
	{
		services.AddApiVersioning(options =>
		{
			options.AssumeDefaultVersionWhenUnspecified = true;
			options.DefaultApiVersion = Versioning.ApiVersion;
			options.ReportApiVersions = true;
			options.ApiVersionReader = new HeaderApiVersionReader(HHC.Version);
		});

		return services;
	}
}
