using Database.Adapter.Entities.Contexts.MasterData;
using Database.Adapter.Repositories;
using Database.Adapter.Repositories.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Xml.Serialization;
using static Database.Adapter.Entities.Constants.XmlConstants;

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
			List<CalendarDay> calendarDaysByYear = masterDataRepository.CalendarRepository.GetManyByCondition(
				expression: x => x.Year.Equals(2023),
				includeProperties: new[] { nameof(CalendarDay.DayType) }
				).ToList();

			XmlSerializer xmlSerializer = new(typeof(List<CalendarDay>));
			using (StreamWriter writer = new("CalendarDaysByYear.xml"))
			{
				xmlSerializer.Serialize(writer, calendarDaysByYear, GetXmlSerializerNamespaces());
			}

			List<CalendarDay> allCalendarDays = masterDataRepository.CalendarRepository.GetAll().ToList();
			xmlSerializer = new(typeof(List<CalendarDay>));
			using (StreamWriter writer = new("AllCalendarDays.xml"))
			{
				xmlSerializer.Serialize(writer, allCalendarDays, GetXmlSerializerNamespaces());
			}

			List<DayType> allDayTypes = masterDataRepository.DayTypeRepository.GetAll().ToList();
			xmlSerializer = new XmlSerializer(typeof(List<DayType>));
			using (StreamWriter writer = new("AllDayTypes.xml"))
			{
				xmlSerializer.Serialize(writer, allDayTypes, GetXmlSerializerNamespaces());
			}

			List<DayType> allActiveDayTypes = masterDataRepository.DayTypeRepository.GetAllActive().ToList();
			xmlSerializer = new XmlSerializer(typeof(List<DayType>));
			using (StreamWriter writer = new("AllActiveDayTypes.xml"))
			{
				xmlSerializer.Serialize(writer, allActiveDayTypes, GetXmlSerializerNamespaces());
			}

			CalendarDay? calendarDay = masterDataRepository.CalendarRepository.GetByDate(DateTime.UtcNow);

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
				_ = masterDataRepository.CalendarRepository.Create(newCalendarDays.OrderBy(x => x.Date));
				_ = masterDataRepository.CommitChanges();
			}
			calendarDay = masterDataRepository.CalendarRepository.GetByDate(DateTime.UtcNow);

			_logger.LogInformation("Worker running at: {time} - {day}", DateTimeOffset.Now, calendarDay.WeekDayName);

			await Task.Delay(10000, stoppingToken);
		}
	}
}
