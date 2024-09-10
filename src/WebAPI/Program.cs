using Application.Installer;

using Infrastructure.Installer;

using Presentation.Installer;

using WebAPI.Extensions;

namespace WebAPI;

internal sealed class Program
{
	[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
	internal static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.ConfigureInfrastructureServices(builder.Configuration, builder.Environment);
		builder.Services.ConfigureApplicationServices();
		builder.Services.ConfigurePresentationServices();

		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.ConfigureSwagger();

		WebApplication app = builder.Build();
		// Configure the HTTP request pipeline.
		if (!app.Environment.IsProduction())
		{
			app.ConfigureSwaggerUI();
		}

		app.UseHttpsRedirection();
		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();
		app.Run();
	}
}
