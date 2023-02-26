using DA.Infrastructure.Installer;
using DA.Repositories.Installer;
using Debug.ConsoleApp.Services;
using Debug.ConsoleApp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Style", "IDE0058", Justification = "DEBUG - Not relevant here.")]
internal sealed class Program
{
	private static async Task Main(string[] args)
	{
		using IHost host = Host.CreateDefaultBuilder(args)
			.ConfigureServices(services =>
			{
				services.AddTransient<IExampleTransientService, ExampleTransientService>();
				services.AddScoped<IExampleScopedService, ExampleScopedService>();
				services.AddSingleton<IExampleSingletonService, ExampleSingletonService>();
				services.AddTransient<ServiceLifetimeReporter>();

				services.AddInfrastructureServices();
				services.AddRepositoryManager();
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