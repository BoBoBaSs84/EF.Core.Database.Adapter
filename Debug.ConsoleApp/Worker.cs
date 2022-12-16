using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories;
using Database.Adapter.Repositories.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Debug.ConsoleApp;

public class Worker : BackgroundService
{
	private readonly ILogger<Worker> _logger;
	private readonly IMasterRepository masterRepository;
	private readonly IMasterDataRepository masterDataRepository;

	public Worker(ILogger<Worker> logger)
	{
		_logger = logger;
		masterRepository = new MasterRepository();
		masterDataRepository = new MasterDataRepository();
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

			var calendar = masterDataRepository.CalendarRepository.GetByCondition(x => x.Date == DateTime.Parse("2022-12-02"));
			if (calendar is null)
			{
				masterDataRepository.CalendarRepository.Create(new Database.Adapter.Entities.MasterData.Calendar() { Date = DateTime.Parse("2022-12-02") });
				int i = masterDataRepository.CommitChanges();
				Console.WriteLine(i);
			}
			else
			{
				Console.WriteLine(calendar.Id);
			}

			await Task.Delay(1000, stoppingToken);
		}
	}
}
