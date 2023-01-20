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
/// Inherits from the <see cref="GenericRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="ICalendarDayRepository"/> interface</item>
/// </list>
/// </remarks>
internal sealed class CalendarDayRepository : GenericRepository<CalendarDay>, ICalendarDayRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CalendarDayRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
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
	public IEnumerable<CalendarDay> GetByDate(DateTime minDate, DateTime maxDate, bool trackChanges = false) =>
		GetManyByCondition(
			expression: x => x.Date >= minDate.ToSqlDate() && x.Date <= maxDate.ToSqlDate(),
			trackChanges: trackChanges
			);
	/// <inheritdoc/>
	public IEnumerable<CalendarDay> GetByDate(IEnumerable<DateTime> dates, bool trackChanges = false) =>
		GetManyByCondition(
			expression: x => dates.Contains(x.Date),
			trackChanges: trackChanges
			);
	/// <inheritdoc/>
	public IEnumerable<CalendarDay> GetByDayType(string dayTypeName, bool trackChanges = false) =>
		GetManyByCondition(
			expression: x => x.DayType.Name.Contains(dayTypeName),
			trackChanges: trackChanges
			);
	/// <inheritdoc/>
	public IEnumerable<CalendarDay> GetByDayType(int dayTypeId, bool trackChanges = false) =>
		GetManyByCondition(
			expression: x => x.DayTypeId.Equals(dayTypeId),
			trackChanges: trackChanges
			);
}
