using Domain.Models.Common;

namespace Domain.Extensions;

public static partial class ModelExtensions
{
	/// <summary>
	/// Filters the calendar entries by year.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="year">The year to be filtered.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<CalendarModel> FilterByYear(this IQueryable<CalendarModel> query, int? year) =>
		year.HasValue ? query.Where(x => x.Date.Year.Equals(year)) : query;

	/// <summary>
	/// Filters the calendar entries by month.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="month">The month to be filtered.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<CalendarModel> FilterByMonth(this IQueryable<CalendarModel> query, int? month) =>
		month.HasValue ? query.Where(x => x.Date.Month.Equals(month)) : query;

	/// <summary>
	/// Filters the calendar entries by date range.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="minDate">The minimum date.</param>
	/// <param name="maxDate">The maximum date.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<CalendarModel> FilterByDateRange(this IQueryable<CalendarModel> query, DateTime? minDate, DateTime? maxDate)
	{
		query = minDate.HasValue ? query.Where(x => x.Date >= minDate.ToSqlDate()) : query;
		query = maxDate.HasValue ? query.Where(x => x.Date <= maxDate.ToSqlDate()) : query;
		return query;
	}
}
