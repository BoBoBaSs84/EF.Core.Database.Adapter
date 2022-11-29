using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.Data;

namespace Debug.ConsoleApp;

internal class Program
{
	private static void Main(string[] args)
	{
		MasterDataRepositoryManager masterDataRepository = new();
		IQueryable<Calendar> test = masterDataRepository.CalendarRepository.FindAll();

		Console.WriteLine("Hello, World!");
	}
}
