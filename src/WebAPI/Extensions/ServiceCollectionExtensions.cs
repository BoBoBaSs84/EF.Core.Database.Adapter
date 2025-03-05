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
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterSwaggerConfiguration(this IServiceCollection services)
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
