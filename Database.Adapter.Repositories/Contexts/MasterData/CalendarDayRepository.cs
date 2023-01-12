using Database.Adapter.Entities.Contexts.MasterData;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Contexts.MasterData.Interfaces;
using Database.Adapter.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Repositories.Contexts.MasterData;

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
	/// <inheritdoc/>
	public CalendarDay GetByDate(DateTime dateTime, bool trackChanges = false) =>
		GetByCondition(
			expression: x => x.Date.Equals(dateTime.ToSqlDate()),
			trackChanges: trackChanges
			);
	/// <inheritdoc/>
	public IEnumerable<CalendarDay> GetByDateRange(DateTime minDate, DateTime maxDate, bool trackChanges = false) =>
		GetManyByCondition(
			expression: x => x.Date >= minDate.ToSqlDate()
			&& x.Date <= maxDate.ToSqlDate(),
			trackChanges: trackChanges
			);
}
