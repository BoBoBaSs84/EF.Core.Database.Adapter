using Application.Interfaces.Presentation.Services;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Presentation.Common;
using Presentation.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using HttpHeaders = Presentation.Constants.PresentationConstants.HttpHeaders;

namespace Presentation.Extensions;

/// <summary>
/// The service collection extensions class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, dependency injection.")]
internal static class ServiceCollectionExtensions
{
	/// <summary>
	/// Enriches a service collection with the presentation singleton services.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection ConfigureSingletonServices(this IServiceCollection services)
	{
		services.TryAddSingleton<ICurrentUserService, CurrentUserService>();

		return services;
	}

	/// <summary>
	/// Enriches a service collection with the presentation controllers.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection ConfigureApiControllers(this IServiceCollection services)
	{
		services.AddControllers()
			.AddApplicationPart(typeof(IPresentationAssemblyMarker).Assembly)
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
				options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
				options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
			});

		return services;
	}

	/// <summary>
	/// Enriches a service collection with the api versioning.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection ConfigureApiVersioning(this IServiceCollection services)
	{
		services.AddApiVersioning(options =>
		{
			options.AssumeDefaultVersionWhenUnspecified = true;
			options.DefaultApiVersion = Versioning.ApiVersion;
			options.ReportApiVersions = true;
			options.ApiVersionReader = new HeaderApiVersionReader(HttpHeaders.Version);
		});

		return services;
	}
}
