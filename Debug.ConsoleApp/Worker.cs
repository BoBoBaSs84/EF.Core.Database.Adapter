using Database.Adapter.Entities.Contexts.Application.MasterData;
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
			CalendarDay? calendarDay = repositoryManager.CalendarRepository.GetByDate(DateTime.UtcNow);

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
				_ = repositoryManager.CalendarRepository.Create(newCalendarDays.OrderBy(x => x.Date));
				_ = repositoryManager.CommitChanges();
			}
			calendarDay = repositoryManager.CalendarRepository.GetByDate(DateTime.UtcNow);

			_logger.LogInformation("Worker running at: {time} - {day}", DateTimeOffset.Now, calendarDay.WeekDayName);

			await Task.Delay(10000, stoppingToken);
		}
	}
}
