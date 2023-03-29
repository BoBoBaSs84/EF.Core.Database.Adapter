using Application.Interfaces.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebAPI.Services;

namespace WebAPI.Extensions;

/// <summary>
/// The service collection extensions class.
/// </summary>
internal static class ServiceCollectionExtensions
{
	/// <summary>
	/// Enriches a service collection with the web api services.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection ConfigureWebApiServices(this IServiceCollection services)
	{
		services.TryAddSingleton<ICurrentUserService, CurrentUserService>();
		return services;
	}
}
