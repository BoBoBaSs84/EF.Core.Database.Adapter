using Application.Interfaces.Infrastructure.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Presentation.Common;
using WebAPI.Services;

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
			options.SwaggerDoc(Versioning.CurrentVersion, new OpenApiInfo()
			{
				Title = "BoBoBaSs84 API",
				Version = Versioning.CurrentVersion
			});

			options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
			{
				In = ParameterLocation.Header,
				Description = "Please enter token",
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				BearerFormat = "JWT",
				Scheme = "Bearer"
			});

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

			string xmlFile = $"{typeof(IPresentationAssemblyMarker).Assembly.GetName().Name}.xml";
			string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

			options.IncludeXmlComments(xmlPath);
		});

		return services;
	}

	/// <summary>
	/// Enriches a service collection with the web api services.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection ConfigureWebApiServices(this IServiceCollection services)
	{
		services.TryAddSingleton<ICurrentUserService, CurrentUserService>();
		return services;
	}
}
