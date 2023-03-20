using Application.Interfaces.Infrastructure.Repositories;
using Domain.Entities.Private;
using Domain.Extensions;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The calendar day repository class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="ICalendarDayRepository"/> interface</item>
/// </list>
/// </remarks>
[SuppressMessage("Globalization", "CA1309",
	Justification = "Translation of the 'string.Equals' overload with a 'StringComparison' parameter is not supported.")]
internal sealed class CalendarDayRepository : IdentityRepository<CalendarDay>, ICalendarDayRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CalendarDayRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public CalendarDayRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public async Task<CalendarDay> GetByDateAsync(DateTime dateTime,
		bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetByConditionAsync(
			expression: x => x.Date.Equals(dateTime.ToSqlDate()),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);

	public async Task<IEnumerable<CalendarDay>> GetByDateAsync(DateTime minDate, DateTime maxDate,
		bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x => x.Date >= minDate.ToSqlDate() && x.Date <= maxDate.ToSqlDate(),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);

	public async Task<IEnumerable<CalendarDay>> GetByDateAsync(IEnumerable<DateTime> dates,
		bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x => dates.Contains(x.Date),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);

	public async Task<IEnumerable<CalendarDay>> GetByDayTypeAsync(string dayTypeName,
		bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x => x.DayType.Name.Equals(dayTypeName),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);

	public async Task<IEnumerable<CalendarDay>> GetByDayTypeAsync(int dayTypeId,
		bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x => x.DayTypeId.Equals(dayTypeId),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);

	public async Task<IEnumerable<CalendarDay>> GetByEndOfMonthAsync(DateTime dateTime,
		bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x => x.EndOfMonth.Equals(ApplicationContext.EndOfMonth(dateTime.ToSqlDate())),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);
}
