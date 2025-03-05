using BB84.Extensions;
using BB84.Home.API.Extensions;
using BB84.Home.Application.Installer;
using BB84.Home.Infrastructure.Installer;
using BB84.Home.Presentation.Installer;

namespace BB84.Home.API;

internal sealed class Program
{
	[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
	internal static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		builder.Services.RegisterApplicationServices()
			.RegisterInfrastructureServices(builder.Configuration, builder.Environment)
			.RegisterPresentationServices();

		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer()
			.RegisterSwaggerConfiguration();

		WebApplication app = builder.Build();
		// Configure the HTTP request pipeline.
		if (app.Environment.IsProduction().IsFalse())
			app.ConfigureSwaggerUI();

		app.UseHttpsRedirection();
		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();
		app.Run();
	}
}
