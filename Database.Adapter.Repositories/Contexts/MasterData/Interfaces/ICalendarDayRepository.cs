using Database.Adapter.Entities.Contexts.Application.MasterData;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.Contexts.MasterData.Interfaces;

/// <summary>
/// The calendar day repository interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface.</item>
/// </list>
/// </remarks>
public interface ICalendarDayRepository : IGenericRepository<CalendarDay>
{
	/// <summary>
	/// Should get the calendar entity by date.
	/// </summary>
	/// <param name="dateTime"></param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns>A calendar entity.</returns>
	CalendarDay GetByDate(DateTime dateTime, bool trackChanges = false);
	/// <summary>
	/// Should get a collection of calendar entities by the provided date range.
	/// </summary>
	/// <param name="minDate">The date to start from.</param>
	/// <param name="maxDate">The date to end with.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns>A collection of calendar entities.</returns>
	IEnumerable<CalendarDay> GetByDateRange(DateTime minDate, DateTime maxDate, bool trackChanges = false);
}
