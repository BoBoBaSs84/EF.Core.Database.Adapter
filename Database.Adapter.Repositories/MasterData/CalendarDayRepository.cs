using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Repositories.MasterData;

/// <summary>
/// The calendar day repository class.
/// </summary>
/// <remarks>
/// Inherits from the following class:
/// <list type="bullet">
/// <item>The <see cref="GenericRepository{TEntity}"/> class</item>
/// </list>
/// </remarks>
internal sealed class CalendarDayRepository : GenericRepository<CalendarDay>, ICalendarDayRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CalendarDayRepository"/> class.
	/// </summary>
	/// <param name="dbContext"></param>
	public CalendarDayRepository(DbContext dbContext) : base(dbContext)
	{
	}
}
