using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Presentation.Common;

using Swashbuckle.AspNetCore.SwaggerGen;

using WebAPI.Common;

using Auth = Presentation.Constants.PresentationConstants.Authentication;

namespace WebAPI.Extensions;

/// <summary>
/// The swagger gen options extensions class
/// </summary>
internal static class SwaggerGenOptionsExtensions
{
	/// <summary>
	/// Enriches a swagger options collection with the type mappings.
	/// </summary>
	/// <param name="options">The swagger options collection to enrich.</param>
	/// <returns>The enriched swagger options collection.</returns>
	internal static SwaggerGenOptions ConfigureTypeMapping(this SwaggerGenOptions options)
	{
		options.MapType<TimeSpan>(() => new OpenApiSchema
		{
			Type = "string",
			Example = new OpenApiString("00:00:00")
		});

		return options;
	}

	/// <summary>
	/// Enriches a swagger options collection with the api documentation.
	/// </summary>
	/// <param name="options">The swagger options collection to enrich.</param>
	/// <returns>The enriched swagger options collection.</returns>
	internal static SwaggerGenOptions ConfigureApiDocumentation(this SwaggerGenOptions options)
	{
		options.SwaggerDoc(Versioning.CurrentVersion, new OpenApiInfo()
		{
			Title = typeof(IWebApiAssemblyMarker).Assembly.GetName().Name,
			Version = Versioning.CurrentVersion,
			Contact = new OpenApiContact() { Name = "Robert Peter Meyer", Url = new Uri("https://github.com/BoBoBaSs84") },
			License = new OpenApiLicense() { Name = "MIT License", Url = new Uri("https://opensource.org/licenses/MIT") }
		});

		string xmlFile = $"{typeof(IPresentationAssemblyMarker).Assembly.GetName().Name}.xml";
		string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

		options.IncludeXmlComments(xmlPath);

		return options;
	}

	/// <summary>
	/// Enriches a swagger options collection with the security definition.
	/// </summary>
	/// <param name="options">The swagger options collection to enrich.</param>
	/// <returns>The enriched swagger options collection.</returns>
	internal static SwaggerGenOptions ConfigureSecurityDefinition(this SwaggerGenOptions options)
	{
		options.AddSecurityDefinition(Auth.Bearer, new OpenApiSecurityScheme()
		{
			In = ParameterLocation.Header,
			Description = "Please enter token",
			Name = Auth.SecuritySchemeName,
			Type = SecuritySchemeType.Http,
			BearerFormat = Auth.BearerFormat,
			Scheme = Auth.Bearer
		});

		return options;
	}

	/// <summary>
	/// Enriches a swagger options collection with the security requirement.
	/// </summary>
	/// <param name="options">The swagger options collection to enrich.</param>
	/// <returns>The enriched swagger options collection.</returns>
	internal static SwaggerGenOptions ConfigureSecurityRequirement(this SwaggerGenOptions options)
	{
		options.AddSecurityRequirement(new OpenApiSecurityRequirement()
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = Auth.Bearer
						}
					},
					Array.Empty<string>()
				}
			});

		return options;
	}
}
