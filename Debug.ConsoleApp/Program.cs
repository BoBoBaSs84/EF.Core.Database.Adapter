using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Debug.ConsoleApp;

internal class Program
{
	private static void Main(string[] args)
	{
		IHost host = Host.CreateDefaultBuilder(args)
			.ConfigureServices(services =>
			{
				_ = services.AddHostedService<Worker>();
			})
			.Build();

		host.Run();
	}
}
