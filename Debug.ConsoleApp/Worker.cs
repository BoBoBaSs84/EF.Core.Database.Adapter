using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories;
using Database.Adapter.Repositories.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Debug.ConsoleApp;

public class Worker : BackgroundService
{
	private readonly ILogger<Worker> _logger;
	private readonly IMasterDataRepository masterDataRepository;

	public Worker(ILogger<Worker> logger)
	{
		_logger = logger;
		masterDataRepository = new MasterDataRepository();
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{

			CalendarDay calendarDay = masterDataRepository.CalendarRepository.GetByCondition(x => x.Date.Equals(new(2023,1,10)));

			_logger.LogInformation("Worker running at: {time} - {day}", DateTimeOffset.Now, calendarDay.WeekDayName);

			await Task.Delay(1000, stoppingToken);
		}
	}
}
