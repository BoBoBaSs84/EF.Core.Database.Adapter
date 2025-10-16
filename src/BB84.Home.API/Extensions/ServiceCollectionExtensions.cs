namespace BB84.Home.API.Extensions;

/// <summary>
/// The service collection extensions class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, dependency injection.")]
internal static class ServiceCollectionExtensions
{
	/// <summary>
	/// Registers the required swagger configuration to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <param name="environment">The hosting environment the application is running in.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterSwaggerConfiguration(this IServiceCollection services, IHostEnvironment environment)
	{
		services.AddSwaggerGen(options =>
		{
			options.ConfigureTypeMapping();
			options.ConfigureSecurityDefinition();
			options.ConfigureSecurityRequirement();
			options.ConfigureApiDocumentation(environment);
		});

		return services;
	}
}
