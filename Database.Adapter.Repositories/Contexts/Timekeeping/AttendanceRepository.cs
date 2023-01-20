﻿using Database.Adapter.Entities.Contexts.Timekeeping;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Contexts.Timekeeping.Interfaces;
using Database.Adapter.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Repositories.Contexts.Timekeeping;

/// <summary>
/// The calendar day repository class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="GenericRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IAttendanceRepository"/> interface</item>
/// </list>
/// </remarks>
internal sealed class AttendanceRepository : GenericRepository<Attendance>, IAttendanceRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="AttendanceRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public AttendanceRepository(DbContext dbContext) : base(dbContext)
	{
	}
	/// <inheritdoc/>
	public IEnumerable<Attendance> GetAllAttendances(int userId, bool trackChanges = false) =>
		GetManyByCondition(
			expression: x => x.UserId.Equals(userId),
			trackChanges: trackChanges
			);
	/// <inheritdoc/>
	public Attendance GetAttendance(int userId, DateTime calendarDate, bool trackChanges = false) =>
		GetByCondition(
			expression: x => x.UserId.Equals(userId) && x.CalendarDay.Date.Equals(calendarDate.ToSqlDate()),
			trackChanges: trackChanges
			);
	/// <inheritdoc/>
	public Attendance GetAttendance(int userId, int calendarDayId, bool trackChanges = false) =>
		GetByCondition(
			expression: x => x.UserId.Equals(userId) && x.CalendarDayId.Equals(calendarDayId),
			trackChanges: trackChanges
			);
}
