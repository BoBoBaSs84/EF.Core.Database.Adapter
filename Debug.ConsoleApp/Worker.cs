using Database.Adapter.Repositories;
using Database.Adapter.Repositories.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Xml.Serialization;

namespace Debug.ConsoleApp;

public class Worker : BackgroundService
{
	private readonly ILogger<Worker> _logger;
	private readonly IMasterDataRepository masterDataRepository;
	private readonly DateTime _today = DateTime.Now;

	public Worker(ILogger<Worker> logger)
	{
		_logger = logger;
		masterDataRepository = new MasterDataRepository();
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

			Database.Adapter.Entities.MasterData.CalendarDay? calendar = masterDataRepository.CalendarRepository.GetByCondition(x => x.Date.Equals(_today));

			if (calendar is null)
			{
				// create some data :)
				List<Database.Adapter.Entities.MasterData.CalendarDay> newCalendarDays = new();
				for (int x = 0; x <= 365; x++)
				{
					Database.Adapter.Entities.MasterData.CalendarDay newcalendarDay = new()
					{
						Date = _today.AddDays(-x),
						DayTypeId = (_today.AddDays(-x).DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday) ? 2 : 1
					};
					newCalendarDays.Add(newcalendarDay);
				}
				masterDataRepository.CalendarRepository.CreateRange(newCalendarDays.OrderBy(x => x.Date));
				int i = masterDataRepository.CommitChanges();
				Console.WriteLine(i);
			}
			else
			{
				XmlSerializer serializer = new(typeof(Database.Adapter.Entities.MasterData.CalendarDay));
				using (StreamWriter writer = new($"{nameof(Database.Adapter.Entities.MasterData.CalendarDay)}.xml"))
				{
					serializer.Serialize(writer, calendar, Database.Adapter.Entities.Constants.XmlConstants.GetXmlSerializerNamespaces());
				}

				Console.WriteLine($"id:{calendar.Id}, date:{calendar.Date}, day:{calendar.Day}");
			}

			List<Database.Adapter.Entities.MasterData.CalendarDay> calendarDaysByYear = masterDataRepository.CalendarRepository.GetByYear(_today.Year).ToList();
			XmlSerializer xmlSerializer = new(typeof(List<Database.Adapter.Entities.MasterData.CalendarDay>));
			using (StreamWriter writer = new("CalendarDaysByYear.xml"))
			{
				xmlSerializer.Serialize(writer, calendarDaysByYear, Database.Adapter.Entities.Constants.XmlConstants.GetXmlSerializerNamespaces());
			}

			List<Database.Adapter.Entities.MasterData.CalendarDay> allCalendarDays = masterDataRepository.CalendarRepository.GetAll().ToList();
			xmlSerializer = new(typeof(List<Database.Adapter.Entities.MasterData.CalendarDay>));
			using (StreamWriter writer = new("AllCalendarDays.xml"))
			{
				xmlSerializer.Serialize(writer, allCalendarDays, Database.Adapter.Entities.Constants.XmlConstants.GetXmlSerializerNamespaces());
			}

			List<Database.Adapter.Entities.MasterData.DayType> allDayTypes = masterDataRepository.DayTypeRepository.GetAll().ToList();
			xmlSerializer = new XmlSerializer(typeof(List<Database.Adapter.Entities.MasterData.DayType>));
			using (StreamWriter writer = new("AllDayTypes.xml"))
			{
				xmlSerializer.Serialize(writer, allDayTypes, Database.Adapter.Entities.Constants.XmlConstants.GetXmlSerializerNamespaces());
			}

			List<Database.Adapter.Entities.MasterData.DayType> allActiveDayTypes = masterDataRepository.DayTypeRepository.GetAllActive().ToList();
			xmlSerializer = new XmlSerializer(typeof(List<Database.Adapter.Entities.MasterData.DayType>));
			using (StreamWriter writer = new("AllActiveDayTypes.xml"))
			{
				xmlSerializer.Serialize(writer, allDayTypes, Database.Adapter.Entities.Constants.XmlConstants.GetXmlSerializerNamespaces());
			}

			await Task.Delay(1000, stoppingToken);
		}
	}
}
