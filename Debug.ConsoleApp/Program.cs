using DA.Domain.Extensions;
using DA.Infrastructure.Installer;
using DA.Repositories.Installer;
using DA.Repositories.Manager.Interfaces;
using Debug.ConsoleApp.Services;
using Debug.ConsoleApp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
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

		//ExemplifyServiceLifetime(host.Services, "Lifetime 1");
		//ExemplifyServiceLifetime(host.Services, "Lifetime 2");

		await GetToday(host.Services);
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

	private static async Task GetToday(IServiceProvider serviceProvider)
	{
		Console.WriteLine("...");
		IRepositoryManager manager = serviceProvider.GetRequiredService<IRepositoryManager>();
		var calendarDay = await manager.CalendarRepository.GetByDateAsync(DateTime.UtcNow);
		Console.WriteLine(calendarDay.ToJsonString());
	}
}