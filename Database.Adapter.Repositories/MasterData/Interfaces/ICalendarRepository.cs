using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.MasterData.Interfaces;

public interface ICalendarRepository : IGenericRepository<Calendar>
{
	IEnumerable<Calendar> GetWithinRange(DateTime start, DateTime end, bool trackChanges = false);
}
