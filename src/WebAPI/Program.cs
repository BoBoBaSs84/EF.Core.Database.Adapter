using Application.Installer;
using Application.Interfaces.Infrastructure.Identity;
using Infrastructure.Installer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Presentation.Common;
using WebAPI.Services;

namespace WebAPI;

internal sealed class Program
{
	[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
	internal static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddInfrastructureServices(builder.Configuration, builder.Environment);
		builder.Services.AddApplicationServices(builder.Configuration, builder.Environment);
		builder.Services.AddAutoMapper();

		builder.Services.TryAddSingleton<ICurrentUserService, CurrentUserService>();

		builder.Services.AddControllers()
			.AddApplicationPart(typeof(IPresentationAssemblyMarker).Assembly);
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(setup =>
		{
			setup.SwaggerDoc(Versioning.CurrentVersion, new OpenApiInfo { Title = "BoBoBaSs84 API", Version = Versioning.CurrentVersion });

			string xmlFile = $"{typeof(IPresentationAssemblyMarker).Assembly.GetName().Name}.xml";
			string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

			setup.IncludeXmlComments(xmlPath);
		});

		WebApplication app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsProduction())
		{
			app.UseSwagger();
			app.UseSwaggerUI(setup =>
			{
				setup.SwaggerEndpoint($"/swagger/{Versioning.CurrentVersion}/swagger.json", Versioning.CurrentVersion);
			});
		}

		app.UseHttpsRedirection();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}
