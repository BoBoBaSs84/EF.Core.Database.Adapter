using Domain.Enumerators;
using Domain.Models.Attendance;

namespace Domain.Extensions;

public static partial class ModelExtensions
{
	/// <summary>
	/// Filters the attendance entries by year.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="year">The year to be filtered.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<AttendanceModel> FilterByYear(this IQueryable<AttendanceModel> query, int? year) =>
		year.HasValue ? query.Where(x => x.Calendar.Date.Year.Equals(year)) : query;

	/// <summary>
	/// Filter the attendance entries by month.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="month">The month to be filtered.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<AttendanceModel> FilterByMonth(this IQueryable<AttendanceModel> query, int? month) =>
		month.HasValue ? query.Where(x => x.Calendar.Date.Month.Equals(month)) : query;

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
	/// Filter the attendance entries by the attendance type.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="type">The attendance type to be filtered.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<AttendanceModel> FilterByType(this IQueryable<AttendanceModel> query, AttendanceType? type)
		=> type.HasValue ? query.Where(x => x.AttendanceType.Equals(type)) : query;
}
