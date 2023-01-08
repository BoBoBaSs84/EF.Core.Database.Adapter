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
				masterDataRepository.CalendarRepository.Create(new Database.Adapter.Entities.MasterData.CalendarDay() { Date = _today });
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

			List<Database.Adapter.Entities.MasterData.CalendarDay> calendarDays = masterDataRepository.CalendarRepository.GetByYear(_today.Year).ToList();
			XmlSerializer xmlSerializer = new(typeof(List<Database.Adapter.Entities.MasterData.CalendarDay>));
			using (StreamWriter writer = new("CalendarDays.xml"))
			{
				xmlSerializer.Serialize(writer, calendarDays, Database.Adapter.Entities.Constants.XmlConstants.GetXmlSerializerNamespaces());
			}

			List<Database.Adapter.Entities.MasterData.DayType> dayTypes = masterDataRepository.DayTypeRepository.GetAll().ToList();
			xmlSerializer = new XmlSerializer(typeof(List<Database.Adapter.Entities.MasterData.DayType>));
			using (StreamWriter writer = new("DayTypesAll.xml"))
			{
				xmlSerializer.Serialize(writer, dayTypes, Database.Adapter.Entities.Constants.XmlConstants.GetXmlSerializerNamespaces());
			}

			List<Database.Adapter.Entities.MasterData.DayType> dayTypesActive = masterDataRepository.DayTypeRepository.GetAllActive().ToList();
			xmlSerializer = new XmlSerializer(typeof(List<Database.Adapter.Entities.MasterData.DayType>));
			using (StreamWriter writer = new("DayTypesActive.xml"))
			{
				xmlSerializer.Serialize(writer, dayTypes, Database.Adapter.Entities.Constants.XmlConstants.GetXmlSerializerNamespaces());
			}

			await Task.Delay(1000, stoppingToken);
		}
	}
}
