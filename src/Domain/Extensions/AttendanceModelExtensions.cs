using Domain.Models.Attendance;

namespace Domain.Extensions;

/// <summary>
/// The attendance model extensions class.
/// </summary>
public static class AttendanceModelExtensions
{
	/// <summary>
	/// Filters the attendance entries by year.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="year">The year to be filtered.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<AttendanceModel> FilterByYear(this IQueryable<AttendanceModel> query, int? year) =>
		year.HasValue ? query.Where(x => x.Calendar.Year.Equals(year)) : query;

	/// <summary>
	/// Filter the attendance entries by month.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="month">The month to be filtered.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<AttendanceModel> FilterByMonth(this IQueryable<AttendanceModel> query, int? month) =>
		month.HasValue ? query.Where(x => x.Calendar.Month.Equals(month)) : query;

	/// <summary>
	/// Filters the attendance entries by date range.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="minDate">The minimum date.</param>
	/// <param name="maxDate">The maximum date.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<AttendanceModel> FilterByDateRange(this IQueryable<AttendanceModel> query, DateTime? minDate, DateTime? maxDate)
	{
		query = minDate.HasValue ? query.Where(x => x.Calendar.Date >= minDate.ToSqlDate()) : query;
		query = maxDate.HasValue ? query.Where(x => x.Calendar.Date <= maxDate.ToSqlDate()) : query;
		return query;
	}

	/// <summary>
	/// Filters the attendance entries by the end of month.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="endOfMonth">The end of month to be filtered.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<AttendanceModel> FilterByEndOfMonth(this IQueryable<AttendanceModel> query, DateTime? endOfMonth) =>
		endOfMonth.HasValue ? query.Where(x => x.Calendar.EndOfMonth.Equals(endOfMonth.ToSqlDate())) : query;
}
