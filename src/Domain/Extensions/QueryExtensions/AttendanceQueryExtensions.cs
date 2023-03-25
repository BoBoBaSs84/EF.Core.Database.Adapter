using Domain.Entities.Private;

namespace Domain.Extensions.QueryExtensions;

/// <summary>
/// The attendance query extensions class.
/// </summary>
public static class AttendanceQueryExtensions
{
	/// <summary>
	/// Should filter the attendance entities by the year.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="year">The year to be filtered.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<Attendance> FilterByYear(this IQueryable<Attendance> query, int? year) =>
		year.HasValue ? query.Where(x => x.CalendarDay.Year.Equals(year)) : query;

	/// <summary>
	/// Should filter the attendance entities by the month.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="month">The month to be filtered.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<Attendance> FilterByMonth(this IQueryable<Attendance> query, int? month) =>
		month.HasValue ? query.Where(x => x.CalendarDay.Month.Equals(month)) : query;

	/// <summary>
	/// Should filter the attendance entities by a date range.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="minDate">The minimum date.</param>
	/// <param name="maxDate">The maximum date.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<Attendance> FilterByDateRange(this IQueryable<Attendance> query, DateTime? minDate, DateTime? maxDate)
	{
		query = minDate.HasValue ? query.Where(x => x.CalendarDay.Date >= minDate.ToSqlDate()) : query;
		query = maxDate.HasValue ? query.Where(x => x.CalendarDay.Date <= maxDate.ToSqlDate()) : query;
		return query;
	}

	/// <summary>
	/// Should filter the attendance entities by the end of month.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="endOfMonth">The end of month to be filtered.</param>s
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<Attendance> FilterByEndOfMonth(this IQueryable<Attendance> query, DateTime? endOfMonth) =>
		endOfMonth.HasValue ? query.Where(x => x.CalendarDay.EndOfMonth.Equals(endOfMonth.ToSqlDate())) : query;
}
