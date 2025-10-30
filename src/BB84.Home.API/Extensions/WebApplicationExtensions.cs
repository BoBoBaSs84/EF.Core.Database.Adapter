using BB84.Home.Presentation.Common;

using Swagger = BB84.Home.Presentation.Common.PresentationConstants.Swagger;

namespace BB84.Home.API.Extensions;

/// <summary>
/// The web application extensions class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
public static class WebApplicationExtensions
{
	/// <summary>
	/// Should enrich the web application with the swagger ui.
	/// </summary>
	/// <param name="webApplication">The web application to enrich.</param>
	/// <param name="environment">The hosting environment the application is running in.</param>
	/// <returns>The enriched web application.</returns>
	internal static WebApplication ConfigureSwaggerUI(this WebApplication webApplication, IHostEnvironment environment)
	{
		webApplication.UseSwagger();
		webApplication.UseSwaggerUI(setup =>
		{
			setup.SwaggerEndpoint($"/swagger/{Versioning.CurrentVersion}/swagger.json", $"v{Versioning.CurrentVersion}");
			setup.ConfigObject.AdditionalItems.Add(Swagger.SytaxHighlightKey, false);

			if (environment.IsProduction())
				setup.SupportedSubmitMethods([]);
		});

		return webApplication;
	}
}
