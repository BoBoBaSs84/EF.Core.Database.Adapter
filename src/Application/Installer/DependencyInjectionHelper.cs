using Application.Common;
using Application.Interfaces.Application;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
	/// <returns>The enriched service collection.</returns>
	public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
	{
		services.ConfigureAutoMapper();

		services.TryAddScoped<ICalendarDayService, CalendarDayService>();
		services.TryAddScoped<IDayTypeService, DayTypeService>();
		services.TryAddScoped<ICardTypeService, CardTypeService>();

		return services;
	}

	/// <summary>
	/// Enriches a service collection with the auto mapper.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	private static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
	{
		services.AddAutoMapper(typeof(IApplicationAssemblyMarker));
		return services;
	}
}
