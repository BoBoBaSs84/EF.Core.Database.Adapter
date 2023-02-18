using DA.Repositories.Installer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Debug.ConsoleApp;

internal sealed class Program
{
	private static void Main(string[] args)
	{
		IHost host = Host.CreateDefaultBuilder(args)
			.ConfigureServices(services =>
			{
				services.AddHostedService<Worker>();
				services.GetRepositoryManager();
				services.GetIdentityService();
			})
			.ConfigureLogging((context, logging) => _ = logging.AddConsole())
			.Build();

		host.Run();
	}
}
