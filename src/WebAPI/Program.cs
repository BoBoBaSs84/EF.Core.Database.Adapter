using Application.Common.Interfaces.Identity;
using Infrastructure.Installer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebAPI.Services;

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
		builder.Services.AddSwaggerGen();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsProduction())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();


		app.MapControllers();

		app.Run();
	}
}
