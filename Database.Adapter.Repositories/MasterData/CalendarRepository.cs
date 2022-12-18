using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Repositories.MasterData;

internal sealed class CalendarDayRepository : GenericRepository<CalendarDay>, ICalendarDayRepository
{
	public CalendarDayRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public IEnumerable<CalendarDay> GetWithinRange(DateTime start, DateTime end, bool trackChanges = false) =>
		GetManyByCondition(x => x.Date >= start && x.Date <= end, trackChanges);
}
