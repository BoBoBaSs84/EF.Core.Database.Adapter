using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.Data;

namespace Debug.ConsoleApp;

internal class Program
{
	private static void Main(string[] args)
	{
		MasterDataRepository masterDataRepository = new();

		masterDataRepository.CalendarRepository.Create(new Calendar() { Date = DateTime.Now });
		int i = masterDataRepository.CommitChanges();
		Console.WriteLine(i);
		Console.WriteLine("Hello, World!");
	}
}
