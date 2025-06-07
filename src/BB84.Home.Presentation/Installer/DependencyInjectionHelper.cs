using BB84.Home.Presentation.Extensions;

using Microsoft.Extensions.DependencyInjection;

namespace BB84.Home.Presentation.Installer;

/// <summary>
/// Provides helper methods for configuring dependency injection in an application.
/// </summary>
/// <remarks>
/// This class contains extension methods for registering services to an <see cref="IServiceCollection"/>.
/// It is intended to simplify the setup of dependency injection for presentation-layer components.
/// </remarks>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, dependency injection.")]
public static class DependencyInjectionHelper
{
	/// <summary>
	/// Registers the required presentation services to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to which the singleton services will be added.</param>
	/// <returns>The same <see cref="IServiceCollection"/> instance so that multiple calls can be chained.</returns>
	public static IServiceCollection RegisterPresentationServices(this IServiceCollection services)
	{
		services.RegisterApiVersionConfiguration()
			.RegisterControllerConfiguration()
			.RegisterSingletonServices();

		return services;
	}
}
