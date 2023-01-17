using Database.Adapter.Entities.Contexts.Application.Timekeeping;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.Contexts.Application.Timekeeping.Interfaces;

/// <summary>
/// The attendance repository interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface.</item>
/// </list>
/// </remarks>
internal interface IAttendanceRepository : IGenericRepository<Attendance>
{
	/// <summary>
	/// Should get the attendance entity by user and calendar identifier.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="calendarDayId">The identifier of the calendar day.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns>A attendance entity.</returns>
	Attendance GetAttendanceByUserId(int userId, int calendarDayId, bool trackChanges = false);
	/// <summary>
	/// Should get a collection of attendance entities by the user identifier.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns>A collection of attendance entities.</returns>
	IEnumerable<Attendance> GetAllAttendancesByUserId(int userId, bool trackChanges = false);
}
