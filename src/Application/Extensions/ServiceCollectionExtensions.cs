﻿using Application.Common;
using Application.Interfaces.Application.Attendance;
using Application.Interfaces.Application.Common;
using Application.Interfaces.Application.Finance;
using Application.Interfaces.Application.Identity;
using Application.Interfaces.Application.Todo;
using Application.Options;
using Application.Services.Attendance;
using Application.Services.Common;
using Application.Services.Finance;
using Application.Services.Identity;
using Application.Services.Todo;

using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Application.Extensions;

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
		services.TryAddScoped<ITransactionService, TransactionService>();
		services.TryAddScoped<ITodoService, TodoService>();
		services.TryAddScoped<ITokenService, TokenService>();

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
		services.TryAddSingleton<IDateTimeService, DateTimeService>();
		services.TryAddSingleton<IEnumeratorService, EnumeratorService>();

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
