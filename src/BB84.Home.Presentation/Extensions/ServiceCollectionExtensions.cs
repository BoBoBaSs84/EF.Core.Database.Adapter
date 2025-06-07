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
/// Provides extension methods for configuring services in an <see cref="IServiceCollection"/>.
/// </summary>
/// <remarks>
/// This class contains methods to register singleton services, controller configurations and API
/// versioning settings to an <see cref="IServiceCollection"/>. These methods are intended to simplify
/// the setup of dependency injection and application configuration in ASP.NET Core applications.
/// </remarks>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, dependency injection.")]
internal static class ServiceCollectionExtensions
{
	/// <summary>
	/// Registers singleton services required for the application.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to which the singleton services will be added.</param>
	/// <returns>The same <see cref="IServiceCollection"/> instance so that multiple calls can be chained.</returns>
	internal static IServiceCollection RegisterSingletonServices(this IServiceCollection services)
	{
		services.TryAddSingleton<ICurrentUserService, CurrentUserService>();

		return services;
	}

	/// <summary>
	/// Registers the required controller configuration to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to which the singleton services will be added.</param>
	/// <returns>The same <see cref="IServiceCollection"/> instance so that multiple calls can be chained.</returns>
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
	/// <param name="services">The <see cref="IServiceCollection"/> to which the singleton services will be added.</param>
	/// <returns>The same <see cref="IServiceCollection"/> instance so that multiple calls can be chained.</returns>
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
