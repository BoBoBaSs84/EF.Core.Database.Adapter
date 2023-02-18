using DA.Models.Contexts.MasterData;
using DA.Repositories.Manager.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Debug.ConsoleApp;

public class Worker : BackgroundService
{
	private readonly ILogger<Worker> _logger;
	private readonly IRepositoryManager _repositoryManager;

	public Worker(ILogger<Worker> logger, IRepositoryManager repositoryManager)
	{
		_logger = logger;
		_repositoryManager = repositoryManager;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			CalendarDay? calendarDay = await _repositoryManager.CalendarRepository.GetByDateAsync(DateTime.UtcNow, cancellationToken: stoppingToken);

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
				await _repositoryManager.CalendarRepository.CreateAsync(newCalendarDays.OrderBy(x => x.Date), stoppingToken);
				_ = await _repositoryManager.CommitChangesAsync(stoppingToken);
				calendarDay = await _repositoryManager.CalendarRepository.GetByDateAsync(DateTime.UtcNow, cancellationToken: stoppingToken);
			}

			_logger.LogInformation("Worker running at: {time} - {day}", DateTimeOffset.Now, calendarDay.WeekDayName);

			await Task.Delay(10000, stoppingToken);
		}
	}
}
