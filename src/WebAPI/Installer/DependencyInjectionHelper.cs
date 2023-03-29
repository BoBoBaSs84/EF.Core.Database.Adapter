using WebAPI.Extensions;

namespace WebAPI.Installer;

/// <summary>
/// Helper class for web api dependency injection.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static class DependencyInjectionHelper
{
	/// <summary>
	/// Enriches a service collection with the swagger services.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection ConfigureSwagger(this IServiceCollection services)
	{
		services.AddSwaggerGen(options =>
		{
			options.ConfigureTypeMapping();
			options.ConfigureSecurityDefinition();
			options.ConfigureSecurityRequirement();
			options.ConfigureApiDocumentation();
		});

		return services;
	}
}
