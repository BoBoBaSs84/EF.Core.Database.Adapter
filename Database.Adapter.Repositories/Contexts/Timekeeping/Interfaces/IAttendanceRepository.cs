﻿using Database.Adapter.Entities.Contexts.Timekeeping;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.Contexts.Timekeeping.Interfaces;

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
	/// Should get the attendance entity by user and calendar identifier.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="calendarDayId">The identifier of the calendar day.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns>A attendance entity.</returns>
	Attendance GetAttendance(int userId, int calendarDayId, bool trackChanges = false);
	/// <summary>
	/// Should get the attendance entity by user and calendar date.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="calendarDate">The date of the calendar day.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns>A attendance entity.</returns>
	Attendance GetAttendance(int userId, DateTime calendarDate, bool trackChanges = false);
	/// <summary>
	/// Should get a collection of attendance entities by the user identifier.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns>A collection of attendance entities.</returns>
	IEnumerable<Attendance> GetAllAttendances(int userId, bool trackChanges = false);
}
