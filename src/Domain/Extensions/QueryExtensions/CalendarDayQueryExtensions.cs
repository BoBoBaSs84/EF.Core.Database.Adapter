using Domain.Entities.Private;

namespace Domain.Extensions.QueryExtensions;

/// <summary>
/// The calendar day query extensions class.
/// </summary>
public static class CalendarDayQueryExtensions
{
	/// <summary>
	/// Should filter the calendar day entities by the year.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="year">The year to be filtered.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<CalendarDay> FilterByYear(this IQueryable<CalendarDay> query, int? year) =>
		year.HasValue ? query.Where(x => x.Year.Equals(year)) : query;

	/// <summary>
	/// Should filter the calendar day entities by the month.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="month">The month to be filtered.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<CalendarDay> FilterByMonth(this IQueryable<CalendarDay> query, int? month) =>
		month.HasValue ? query.Where(x => x.Month.Equals(month)) : query;

	/// <summary>
	/// Should filter the calendar day entities by a date range.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="minDate">The minimum date.</param>
	/// <param name="maxDate">The maximum date.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<CalendarDay> FilterByDateRange(this IQueryable<CalendarDay> query, DateTime? minDate, DateTime? maxDate)
	{
		query = minDate.HasValue ? query.Where(x => x.Date >= minDate) : query;
		query = maxDate.HasValue ? query.Where(x => x.Date <= maxDate) : query;
		return query;
	}
}
