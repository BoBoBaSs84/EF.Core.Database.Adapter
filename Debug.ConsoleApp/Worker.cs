using Database.Adapter.Entities.Contexts.MasterData;
using Database.Adapter.Repositories;
using Database.Adapter.Repositories.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Debug.ConsoleApp;

public class Worker : BackgroundService
{
	private readonly ILogger<Worker> _logger;
	private readonly IRepositoryManager repositoryManager;

	public Worker(ILogger<Worker> logger)
	{
		_logger = logger;
		repositoryManager = new RepositoryManager();
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			CalendarDay? calendarDay = await repositoryManager.CalendarRepository.GetByDateAsync(DateTime.UtcNow, cancellationToken: stoppingToken);

			if (calendarDay is null)
			{
				// create some data :)
				List<CalendarDay> newCalendarDays = new();
				DateTime startDate = new(1900, 1, 1)
					, endDate = startDate.AddYears(200);

				while (startDate <= endDate)
				{
					CalendarDay newcalendarDay = new()
					{
						Date = startDate,
						DayTypeId = (startDate.DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday) ? 2 : 1
					};
					newCalendarDays.Add(newcalendarDay);
					startDate = startDate.AddDays(1);
				}
				await repositoryManager.CalendarRepository.CreateAsync(newCalendarDays.OrderBy(x => x.Date), stoppingToken);
				_ = await repositoryManager.CommitChangesAsync(stoppingToken);
				calendarDay = await repositoryManager.CalendarRepository.GetByDateAsync(DateTime.UtcNow, cancellationToken: stoppingToken);
			}

			_logger.LogInformation("Worker running at: {time} - {day}", DateTimeOffset.Now, calendarDay.WeekDayName);

			await Task.Delay(10000, stoppingToken);
		}
	}
}
