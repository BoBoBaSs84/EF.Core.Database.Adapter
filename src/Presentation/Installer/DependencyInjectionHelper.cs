using Microsoft.Extensions.DependencyInjection;

using Presentation.Extensions;

namespace Presentation.Installer;

/// <summary>
/// The dependency injection helper class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, dependency injection.")]
public static class DependencyInjectionHelper
{
	/// <summary>
	/// Registers the required presentation services to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	public static IServiceCollection RegisterPresentationServices(this IServiceCollection services)
	{
		services.RegisterApiVersionConfiguration()
			.RegisterControllerConfiguration()
			.RegisterSingletonServices();

		return services;
	}
}
