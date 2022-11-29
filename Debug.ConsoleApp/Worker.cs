using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Debug.ConsoleApp;

public class Worker : BackgroundService
{
	private readonly ILogger<Worker> _logger;

	public Worker(ILogger<Worker> logger)
	{
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

			MasterDataRepository masterDataRepository = new();

			masterDataRepository.CalendarRepository.Create(new Calendar() { Date = DateTime.Now });
			int i = masterDataRepository.CommitChanges();
			Console.WriteLine(i);

			await Task.Delay(1000, stoppingToken);
		}
	}
}
