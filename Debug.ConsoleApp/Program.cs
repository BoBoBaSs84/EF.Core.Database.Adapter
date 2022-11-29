using Database.Adapter.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
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
				_ = services.AddDbContextFactory<MasterDataContext>();
				_ = services.AddDbContext<MasterDataContext>(opt =>
				{
					_ = opt.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MasterData;Integrated Security=True;");
					_ = opt.EnableDetailedErrors(true);
					_ = opt.EnableSensitiveDataLogging(true);
				});
				_ = services.AddDbContext<AuthenticationContext>(opt =>
				{
					_ = opt.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Authentication;Integrated Security=True;");
					_ = opt.EnableDetailedErrors(true);
					_ = opt.EnableSensitiveDataLogging(true);
				});
			})
			.Build();

		host.Run();
	}
}
