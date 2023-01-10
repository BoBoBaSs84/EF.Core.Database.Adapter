using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Debug.ConsoleApp;

internal class Program
{
	private static void Main(string[] args)
	{
		IHost host = Host.CreateDefaultBuilder(args)
			.ConfigureServices(services => services.AddHostedService<Worker>())
			.ConfigureLogging((context, logging) => _ = logging.AddConsole())
			.Build();

		host.Run();
	}
}
