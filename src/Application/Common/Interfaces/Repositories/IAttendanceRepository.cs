﻿using Application.Common.Interfaces.Repositories.BaseTypes;
using Domain.Entities.Private;

namespace Application.Common.Interfaces.Repositories;

/// <summary>
/// The attendance repository interface.
/// </summary>
/// <remarks>
/// Derives from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IIdentityRepository{TIdentityEntity}"/> interface</item>
/// </list>
/// </remarks>
public interface IAttendanceRepository : IIdentityRepository<Attendance>
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
