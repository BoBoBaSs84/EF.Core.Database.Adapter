using Database.Adapter.Repositories;
using Database.Adapter.Repositories.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Xml.Serialization;

namespace Debug.ConsoleApp;

public class Worker : BackgroundService
{
	private readonly ILogger<Worker> _logger;
	private readonly IMasterRepository masterRepository;
	private readonly IMasterDataRepository masterDataRepository;
	private readonly DateTime _today = DateTime.Now;

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

			Database.Adapter.Entities.MasterData.Calendar? calendar = masterDataRepository.CalendarRepository.GetByCondition(x => x.Date.Equals(_today));

			if (calendar is null)
			{
				masterDataRepository.CalendarRepository.Create(new Database.Adapter.Entities.MasterData.Calendar() { Date = _today });
				int i = masterDataRepository.CommitChanges();
				Console.WriteLine(i);
			}
			else
			{
				XmlSerializer serializer = new(typeof(Database.Adapter.Entities.MasterData.Calendar));
				using (StreamWriter writer = new($"{nameof(Database.Adapter.Entities.MasterData.Calendar)}.xml"))
				{
					serializer.Serialize(writer, calendar);
				}

				Console.WriteLine($"id:{calendar.Id}, date:{calendar.Date}, day:{calendar.Day}");
			}

			List<Database.Adapter.Entities.MasterData.Calendar> calendarDays = masterDataRepository.CalendarRepository.GetAll().ToList();
			XmlSerializer xmlSerializer = new(typeof(List<Database.Adapter.Entities.MasterData.Calendar>));
			using (StreamWriter writer = new("CalendarDays.xml"))
			{
				xmlSerializer.Serialize(writer, calendarDays);
			}

			await Task.Delay(1000, stoppingToken);
		}
	}
}
