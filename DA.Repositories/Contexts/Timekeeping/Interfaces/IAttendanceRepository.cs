using DA.Models.Contexts.Timekeeping;
using DA.Repositories.BaseTypes.Interfaces;

namespace DA.Repositories.Contexts.Timekeeping.Interfaces;

/// <summary>
/// The attendance repository interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface.</item>
/// </list>
/// </remarks>
public interface IAttendanceRepository : IGenericRepository<Attendance>
{
	/// <summary>
	/// Should get one attendance entity by user and calendar identifier.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="calendarDayId">The identifier of the calendar day.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A attendance entity.</returns>
	Task<Attendance> GetAttendanceAsync(int userId, int calendarDayId, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should get one attendance entity by user and calendar date.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="calendarDate">The date of the calendar day.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A attendance entity.</returns>
	Task<Attendance> GetAttendanceAsync(int userId, DateTime calendarDate, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should get a collection of attendance entities by the user identifier.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A collection of attendance entities.</returns>
	Task<IEnumerable<Attendance>> GetAttendancesAsync(int userId, bool trackChanges = false, CancellationToken cancellationToken = default);
}
