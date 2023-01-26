using DA.Models.Extensions;
using DA.Repositories.BaseTypes;
using DA.Repositories.Contexts.Timekeeping.Interfaces;
using DA.Models.Contexts.Timekeeping;
using Microsoft.EntityFrameworkCore;

namespace DA.Repositories.Contexts.Timekeeping;

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

	public async Task<IEnumerable<Attendance>> GetAttendancesAsync(int userId,
		bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x => x.UserId.Equals(userId),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);

	public async Task<Attendance> GetAttendanceAsync(int userId, DateTime calendarDate,
		bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetByConditionAsync(
			expression: x => x.UserId.Equals(userId) && x.CalendarDay.Date.Equals(calendarDate.ToSqlDate()),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);

	public async Task<Attendance> GetAttendanceAsync(int userId, int calendarDayId,
		bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetByConditionAsync(
			expression: x => x.UserId.Equals(userId) && x.CalendarDayId.Equals(calendarDayId),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);
}
