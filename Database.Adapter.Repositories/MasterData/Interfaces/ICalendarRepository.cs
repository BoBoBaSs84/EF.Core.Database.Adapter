using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.MasterData.Interfaces;

public interface ICalendarDayRepository : IGenericRepository<CalendarDay>
{
	IEnumerable<CalendarDay> GetWithinRange(DateTime start, DateTime end, bool trackChanges = false);
}
