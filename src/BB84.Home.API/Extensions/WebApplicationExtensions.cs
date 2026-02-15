using BB84.Home.Presentation.Common;

using Microsoft.OpenApi;

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
		webApplication.UseSwagger(setup =>
		{
			setup.OpenApiVersion = OpenApiSpecVersion.OpenApi3_1;
			setup.RouteTemplate = "swagger/{documentName}/swagger.json";
		});

		webApplication.UseSwaggerUI(setup =>
		{
			setup.DocumentTitle = "BB84.Home.API";
			setup.ConfigObject.AdditionalItems.Add(Swagger.SytaxHighlightKey, false);
			setup.ShowCommonExtensions();
			setup.ShowExtensions();
			setup.SwaggerEndpoint($"/swagger/{Versioning.CurrentVersion}/swagger.json", $"v{Versioning.CurrentVersion}");
		});

		return webApplication;
	}
}
