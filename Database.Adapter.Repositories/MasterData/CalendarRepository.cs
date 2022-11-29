using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Repositories.MasterData;

internal sealed class CalendarRepository : GenericRepository<Calendar>, ICalendarRepository
{
	public CalendarRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public IEnumerable<Calendar> GetWithinRange(DateTime start, DateTime end, bool trackChanges = false) =>
		GetManyByCondition(x => x.Date >= start && x.Date <= end, trackChanges);
}
