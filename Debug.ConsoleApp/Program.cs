using Application.Interfaces.Infrastructure.Identity;
using Debug.ConsoleApp.Services;
using Debug.ConsoleApp.Services.Interfaces;
using Infrastructure.Extensions;
using Infrastructure.Installer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Style", "IDE0058", Justification = "DEBUG - Not relevant here.")]
internal sealed class Program
{
	private static async Task Main(string[] args)
	{
		using IHost host = Host.CreateDefaultBuilder(args)
			.ConfigureAppSettings()
			.ConfigureServices((ctx, services) =>
			{
				services.TryAddSingleton<ICurrentUserService, CurrentUserService>();
				services.TryAddTransient<IExampleTransientService, ExampleTransientService>();
				services.TryAddScoped<IExampleScopedService, ExampleScopedService>();
				services.TryAddSingleton<IExampleSingletonService, ExampleSingletonService>();
				services.TryAddTransient<ServiceLifetimeReporter>();

				services.AddInfrastructureServices(ctx.Configuration, ctx.HostingEnvironment);
			}).Build();

		ExemplifyServiceLifetime(host.Services, "Lifetime 1");
		ExemplifyServiceLifetime(host.Services, "Lifetime 2");

		await host.RunAsync();
	}

	private static void ExemplifyServiceLifetime(IServiceProvider hostProvider, string lifetime)
	{
		using IServiceScope serviceScope = hostProvider.CreateScope();
		IServiceProvider provider = serviceScope.ServiceProvider;
		ServiceLifetimeReporter logger = provider.GetRequiredService<ServiceLifetimeReporter>();
		logger.ReportServiceLifetimeDetails(
				$"{lifetime}: Call 1 to provider.GetRequiredService<ServiceLifetimeLogger>()");

		Console.WriteLine("...");

		logger = provider.GetRequiredService<ServiceLifetimeReporter>();
		logger.ReportServiceLifetimeDetails(
				$"{lifetime}: Call 2 to provider.GetRequiredService<ServiceLifetimeLogger>()");

		Console.WriteLine();
	}
}