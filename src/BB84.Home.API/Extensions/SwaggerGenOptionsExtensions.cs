﻿using BB84.Home.Application.Common;
using BB84.Home.Presentation.Common;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace BB84.Home.API.Extensions;

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
	/// <param name="environment">The hosting environment the application is running in.</param>
	/// <returns>The enriched swagger options collection.</returns>
	internal static SwaggerGenOptions ConfigureApiDocumentation(this SwaggerGenOptions options, IHostEnvironment environment)
	{
		options.SwaggerDoc(Versioning.CurrentVersion, new OpenApiInfo()
		{
			Description = "My .NET 8 Web API project with Clean Architecture, FluentValidation, EF Core, Identity, JWT Authentication, Swagger and more.",
			Contact = new OpenApiContact() { Name = "Robert Peter Meyer", Url = new Uri("https://github.com/BoBoBaSs84") },
			License = new OpenApiLicense() { Name = "MIT License", Url = new Uri("https://github.com/BoBoBaSs84/EF.Core.Database.Adapter/blob/main/LICENSE") },
			TermsOfService = new Uri("https://github.com/BoBoBaSs84/EF.Core.Database.Adapter/blob/main/LICENSE"),
			Title = $"{environment.ApplicationName} - {environment.EnvironmentName}",
			Version = Versioning.CurrentVersion,
		});

		options.CustomSchemaIds(x => x.FullName);

		List<string> xmlFiles = [
			$"{typeof(IPresentationAssemblyMarker).Assembly.GetName().Name}.xml",
			$"{typeof(IApplicationAssemblyMarker).Assembly.GetName().Name}.xml"
			];

		xmlFiles.ForEach(xml => options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xml)));

		return options;
	}

	/// <summary>
	/// Enriches a swagger options collection with the security definition.
	/// </summary>
	/// <param name="options">The swagger options collection to enrich.</param>
	/// <returns>The enriched swagger options collection.</returns>
	internal static SwaggerGenOptions ConfigureSecurityDefinition(this SwaggerGenOptions options)
	{
		options.AddSecurityDefinition(PresentationConstants.Authentication.Bearer, new OpenApiSecurityScheme()
		{
			In = ParameterLocation.Header,
			Description = "Please enter token",
			Name = PresentationConstants.Authentication.SecuritySchemeName,
			Type = SecuritySchemeType.Http,
			BearerFormat = PresentationConstants.Authentication.BearerFormat,
			Scheme = PresentationConstants.Authentication.Bearer
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
							Id = PresentationConstants.Authentication.Bearer
						}
					},
					Array.Empty<string>()
				}
			});

		return options;
	}
}
