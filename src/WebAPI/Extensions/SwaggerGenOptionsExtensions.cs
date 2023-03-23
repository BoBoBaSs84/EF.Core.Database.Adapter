using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Presentation.Common;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPI.Extensions;

internal static class SwaggerGenOptionsExtensions
{
	internal static SwaggerGenOptions ConfigureTypeMapping(this SwaggerGenOptions options)
	{
		options.MapType<TimeSpan>(() => new OpenApiSchema
		{
			Type = "string",
			Example = new OpenApiString("00:00:00")
		});

		return options;
	}

	internal static SwaggerGenOptions ConfigureApiDocumentation(this SwaggerGenOptions options)
	{
		options.SwaggerDoc(Versioning.CurrentVersion, new OpenApiInfo()
		{
			Title = "BoBoBaSs84 API",
			Version = Versioning.CurrentVersion
		});

		string xmlFile = $"{typeof(IPresentationAssemblyMarker).Assembly.GetName().Name}.xml";
		string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

		options.IncludeXmlComments(xmlPath);

		return options;
	}

	internal static SwaggerGenOptions ConfigureSecurityDefinition(this SwaggerGenOptions options)
	{
		options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
		{
			In = ParameterLocation.Header,
			Description = "Please enter token",
			Name = "Authorization",
			Type = SecuritySchemeType.Http,
			BearerFormat = "JWT",
			Scheme = "Bearer"
		});

		return options;
	}

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
							Id = "Bearer"
						}
					},
					Array.Empty<string>()
				}
			});

		return options;
	}
}
