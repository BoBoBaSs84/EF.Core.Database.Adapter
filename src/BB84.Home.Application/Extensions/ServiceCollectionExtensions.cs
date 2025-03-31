using BB84.Home.Application.Common;
using BB84.Home.Application.Interfaces.Application.Providers;
using BB84.Home.Application.Interfaces.Application.Services.Attendance;
using BB84.Home.Application.Interfaces.Application.Services.Common;
using BB84.Home.Application.Interfaces.Application.Services.Documents;
using BB84.Home.Application.Interfaces.Application.Services.Finance;
using BB84.Home.Application.Interfaces.Application.Services.Identity;
using BB84.Home.Application.Interfaces.Application.Services.Todo;
using BB84.Home.Application.Options;
using BB84.Home.Application.Providers;
using BB84.Home.Application.Services.Attendance;
using BB84.Home.Application.Services.Common;
using BB84.Home.Application.Services.Documents;
using BB84.Home.Application.Services.Finance;
using BB84.Home.Application.Services.Identity;
using BB84.Home.Application.Services.Todo;

using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BB84.Home.Application.Extensions;

/// <summary>
/// The <see cref="IServiceCollection"/> extensions class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, dependency injection.")]
internal static class ServiceCollectionExtensions
{
	/// <summary>
	/// Registers the required scoped services to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterScopedServices(this IServiceCollection services)
	{
		services.TryAddScoped<IAccountService, AccountService>();
		services.TryAddScoped<IAttendanceService, AttendanceService>();
		services.TryAddScoped<IAuthenticationService, AuthenticationService>();
		services.TryAddScoped<ICardService, CardService>();
		services.TryAddScoped<IDocumentService, DocumentService>();
		services.TryAddScoped<ITodoService, TodoService>();
		services.TryAddScoped<ITransactionService, TransactionService>();

		return services;
	}

	/// <summary>
	/// Registers the required singleton services to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterSingletonServices(this IServiceCollection services)
	{
		services.TryAddSingleton<ICalendarService, CalendarService>();
		services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();
		services.TryAddSingleton<IEnumeratorService, EnumeratorService>();

		return services;
	}

	/// <summary>
	/// Registers the required transient services to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterTransientServices(this IServiceCollection services)
	{
		services.TryAddTransient<ITokenService, TokenService>();

		return services;
	}

	/// <summary>
	/// Registers the required auto mapper to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
	{
		services.AddAutoMapper(config => config.AllowNullCollections = true, typeof(IApplicationAssemblyMarker));

		return services;
	}

	/// <summary>
	/// Registers the required fluent validation validators to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterFluentValidation(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation(options => options.DisableDataAnnotationsValidation = true)
			.AddFluentValidationClientsideAdapters();

		services.AddValidatorsFromAssemblyContaining<IApplicationAssemblyMarker>();

		return services;
	}

	/// <summary>
	/// Registers the required application options and settings to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterApplicationOptions(this IServiceCollection services)
	{
		services.AddOptions<BearerSettings>()
			.BindConfiguration(nameof(BearerSettings))
			.ValidateDataAnnotations()
			.ValidateOnStart();

		return services;
	}
}
