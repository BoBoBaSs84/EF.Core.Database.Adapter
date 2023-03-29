using Presentation.Common;
using Swagger = Presentation.Constants.PresentationConstants.Swagger;

namespace WebAPI.Extensions;

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
	/// <returns>The enriched web application.</returns>
	internal static WebApplication ConfigureSwaggerUI(this WebApplication webApplication)
	{
		webApplication.UseSwagger();
		webApplication.UseSwaggerUI(options =>
		{
			options.SwaggerEndpoint($"/swagger/{Versioning.CurrentVersion}/swagger.json", $"v{Versioning.CurrentVersion}");
			options.ConfigObject.AdditionalItems.Add(Swagger.SytaxHighlightKey, false);
		});

		return webApplication;
	}
}
