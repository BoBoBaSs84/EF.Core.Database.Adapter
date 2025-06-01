using System.Text.Json;
using System.Text.Json.Serialization;

using Asp.Versioning;

using BB84.Home.Application.Converters;
using BB84.Home.Application.Interfaces.Presentation.Services;
using BB84.Home.Presentation.Common;
using BB84.Home.Presentation.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BB84.Home.Presentation.Extensions;

/// <summary>
/// The <see cref="IServiceCollection"/> extensions class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, dependency injection.")]
internal static class ServiceCollectionExtensions
{
	/// <summary>
	/// Registers the required singleton services to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterSingletonServices(this IServiceCollection services)
	{
		services.TryAddSingleton<ICurrentUserService, CurrentUserService>();

		return services;
	}

	/// <summary>
	/// Registers the required controller configuration to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterControllerConfiguration(this IServiceCollection services)
	{
		services.AddControllers(options => options.RespectBrowserAcceptHeader = true)
			.AddApplicationPart(typeof(IPresentationAssemblyMarker).Assembly)
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
				options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
				options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
				options.JsonSerializerOptions.Converters.Add(new ByteArrayJsonConverter());
				options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
				//options.JsonSerializerOptions.Converters.Add(new FlagsJsonConverterFactory());
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
				options.JsonSerializerOptions.Converters.Add(new NullableByteArrayJsonConverter());
				options.JsonSerializerOptions.Converters.Add(new NullableDateTimeJsonConverter());
				options.JsonSerializerOptions.Converters.Add(new NullableTimeSpanJsonConverter());
				options.JsonSerializerOptions.Converters.Add(new TimeSpanJsonConverter());
			});

		return services;
	}

	/// <summary>
	/// Registers the required api version configuration to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterApiVersionConfiguration(this IServiceCollection services)
	{
		services.AddApiVersioning(options =>
		{
			options.AssumeDefaultVersionWhenUnspecified = true;
			options.DefaultApiVersion = Versioning.ApiVersion;
			options.ReportApiVersions = true;
			options.ApiVersionReader = new HeaderApiVersionReader(PresentationConstants.HttpHeaders.Version);
		});

		return services;
	}
}
