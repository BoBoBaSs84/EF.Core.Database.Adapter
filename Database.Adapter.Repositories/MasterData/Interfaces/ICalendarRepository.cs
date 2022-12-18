using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.MasterData.Interfaces;

/// <summary>
/// The calendar day repository interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface. The entity is <see cref="CalendarDay"/>.</item>
/// </list>
/// </remarks>
public interface ICalendarDayRepository : IGenericRepository<CalendarDay>
{
	/// <summary>
	/// The method should return a collection of dates within the provided range.
	/// </summary>
	/// <param name="startDate">The first date.</param>
	/// <param name="endDate">The last date.</param>
	/// <param name="trackChanges">Should changes be tracked?</param>
	/// <returns>A range of dates.</returns>
	IEnumerable<CalendarDay> GetWithinRange(DateTime startDate, DateTime endDate, bool trackChanges = false);
	/// <summary>
	/// The method should return a collection of dates within the provided year.
	/// </summary>
	/// <param name="year">The year where the dates should be within.</param>
	/// <param name="trackChanges">Should changes be tracked?</param>
	/// <returns></returns>
	IEnumerable<CalendarDay> GetByYear(int year, bool trackChanges = false);
}
