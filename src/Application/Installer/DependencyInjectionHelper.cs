using Application.Common;
using Application.Interfaces.Application;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Application.Installer;

/// <summary>
/// Helper class for application dependency injection.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, dependency injection.")]
public static class DependencyInjectionHelper
{
	/// <summary>
	/// Enriches a service collection with the application services.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <param name="configuration">The current configuration.</param>
	/// <param name="environment">The hosting environment.</param>
	/// <returns>The enriched service collection.</returns>
	public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
	{
		services.TryAddScoped<IAuthenticationService, AuthenticationService>();
		services.TryAddScoped<ICalendarDayService, CalendarDayService>();
		services.TryAddScoped<IDayTypeService, DayTypeService>();
		services.TryAddScoped<ICardTypeService, CardTypeService>();
		return services;
	}

	/// <summary>
	/// Enriches a service collection with the auto mapper service.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	public static IServiceCollection AddAutoMapper(this IServiceCollection services)
	{
		services.AddAutoMapper(typeof(IApplicationAssemblyMarker));
		return services;
	}
}
