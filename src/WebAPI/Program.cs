using Application.Common.Interfaces.Identity;
using Infrastructure.Installer;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebAPI.Services;
using System.Reflection;

namespace WebAPI;

public class Program
{
	public static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddInfrastructureServices(builder.Configuration, builder.Environment);

		builder.Services.TryAddSingleton<ICurrentUserService, CurrentUserService>();

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(setup =>
		{
			setup.SwaggerDoc("v1", new OpenApiInfo { Title = "BoBoBaSs84 API", Version = "v1" });
			setup.SwaggerDoc("v2", new OpenApiInfo { Title = "BoBoBaSs84 API", Version = "v2" });

			string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

			setup.IncludeXmlComments(xmlPath);
		});

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsProduction())
		{
			app.UseSwagger();
			app.UseSwaggerUI(setup =>
			{
				setup.SwaggerEndpoint("/swagger/v1/swagger.json", "BoBoBaSs84 v1");
				setup.SwaggerEndpoint("/swagger/v2/swagger.json", "BoBoBaSs84 v2");
			});
		}

		app.UseHttpsRedirection();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}
