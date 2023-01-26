using DA.Repositories.BaseTypes.Interfaces;
using DA.Models.Contexts.MasterData;

namespace DA.Repositories.Contexts.MasterData.Interfaces;

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
	/// Should get a calendar entity by date.
	/// </summary>
	/// <param name="dateTime"></param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A calendar entity.</returns>
	Task<CalendarDay> GetByDateAsync(DateTime dateTime, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should get a collection of calendar entities by the provided date range.
	/// </summary>
	/// <param name="minDate">The date to start from.</param>
	/// <param name="maxDate">The date to end with.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A collection of calendar entities.</returns>
	Task<IEnumerable<CalendarDay>> GetByDateAsync(DateTime minDate, DateTime maxDate, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should get a collection of calendar entities by the provided dates.
	/// </summary>
	/// <param name="dates">The list of dates.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A collection of calendar entities.</returns>
	Task<IEnumerable<CalendarDay>> GetByDateAsync(IEnumerable<DateTime> dates, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should get a collection of calendar entities by the day type name.
	/// </summary>
	/// <param name="dayTypeName">The name of the day type.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A collection of calendar entities.</returns>
	Task<IEnumerable<CalendarDay>> GetByDayTypeAsync(string dayTypeName, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should get a collection of calendar entities by the day type identifier.
	/// </summary>
	/// <param name="dayTypeId">The the day identifier.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A collection of calendar entities.</returns>
	Task<IEnumerable<CalendarDay>> GetByDayTypeAsync(int dayTypeId, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should get a collection of calendar entities by the date of the end of the month.
	/// </summary>
	/// <param name="dateTime">A date for which the last day of the month should be used.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A collection of calendar entities.</returns>
	Task<IEnumerable<CalendarDay>> GetByEndOfMonthAsync(DateTime dateTime, bool trackChanges = false, CancellationToken cancellationToken = default);
}
