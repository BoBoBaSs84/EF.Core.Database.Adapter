using BB84.Home.Application.Extensions;

using Microsoft.Extensions.DependencyInjection;

namespace BB84.Home.Application.Installer;

/// <summary>
/// The dependency injection helper class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, dependency injection.")]
public static class DependencyInjectionHelper
{
	/// <summary>
	/// Registers the required application services to the provided <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
	{
		services.RegisterApplicationOptions()
			.RegisterAutoMapper()
			.RegisterFluentValidation()
			.RegisterScopedServices()
			.RegisterSingletonServices()
			.RegisterTransientServices();

		return services;
	}
}
