using Application.Common;
using Application.Interfaces.Application.Attendance;
using Application.Interfaces.Application.Common;
using Application.Interfaces.Application.Finance;
using Application.Interfaces.Application.Identity;
using Application.Interfaces.Application.Todo;
using Application.Services.Attendance;
using Application.Services.Common;
using Application.Services.Finance;
using Application.Services.Identity;
using Application.Services.Todo;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Application.Extensions;

/// <summary>
/// The service collection extensions class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, dependency injection.")]
internal static class ServiceCollectionExtensions
{
	/// <summary>
	/// Enriches a service collection with the scoped services.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection ConfigureScopedServices(this IServiceCollection services)
	{
		services.TryAddScoped<IAccountService, AccountService>();
		services.TryAddScoped<IAttendanceService, AttendanceService>();
		services.TryAddScoped<IAuthenticationService, AuthenticationService>();
		services.TryAddScoped<ICardService, CardService>();
		services.TryAddScoped<ITransactionService, TransactionService>();
		services.TryAddScoped<ITodoService, TodoService>();

		return services;
	}

	/// <summary>
	/// Enriches a service collection with the singleton services.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection ConfigureSingletonServices(this IServiceCollection services)
	{
		services.TryAddSingleton<ICalendarService, CalendarService>();
		services.TryAddSingleton<IDateTimeService, DateTimeService>();
		services.TryAddSingleton<IEnumeratorService, EnumeratorService>();

		return services;
	}

	/// <summary>
	/// Enriches a service collection with the auto mapper.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
	{
		services.AddAutoMapper(typeof(IApplicationAssemblyMarker));

		return services;
	}
}
