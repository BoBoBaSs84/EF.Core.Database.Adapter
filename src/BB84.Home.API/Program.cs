using BB84.Home.API.Extensions;
using BB84.Home.Application.Installer;
using BB84.Home.Infrastructure.Installer;
using BB84.Home.Presentation.Installer;

namespace BB84.Home.API;

internal sealed class Program
{
	[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
	public static async Task Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		builder.Services.RegisterApplicationServices()
			.RegisterInfrastructureServices(builder.Configuration, builder.Environment)
			.RegisterPresentationServices();

		builder.Services.AddEndpointsApiExplorer()
			.RegisterSwaggerConfiguration(builder.Environment);

		WebApplication app = builder.Build();

		app.ConfigureSwaggerUI(builder.Environment);

		app.UseHttpsRedirection();
		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();

		await app.RunAsync()
			.ConfigureAwait(false);
	}
}
