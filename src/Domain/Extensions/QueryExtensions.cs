using Domain.Entities.Enumerator;
using Domain.Entities.Private;

namespace Domain.Extensions;

/// <summary>
/// The calendar day repository extensions class.
/// </summary>
public static class QueryExtensions
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
	/// Should search the day type entities by the description.
	/// </summary>
	/// <param name="query">The query to search.</param>
	/// <param name="description">The description to be searched.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<DayType> SearchByDescription(this IQueryable<DayType> query, string? description) =>
		string.IsNullOrWhiteSpace(description) ? query : query.Where(x => x.Description!.Contains(description));

	/// <summary>
	/// Should search the day type entities by the name.
	/// </summary>
	/// <param name="query">The query to search.</param>
	/// <param name="name">The name to be searched.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<DayType> SearchByName(this IQueryable<DayType> query, string? name) =>
		string.IsNullOrWhiteSpace(name) ? query : query.Where(x => x.Name.Contains(name));

	/// <summary>
	/// Should filter the day type entities if their active.
	/// </summary>
	/// <param name="query">The query to search.</param>
	/// <param name="isActive"><see langword="true"/> or <see langword="false"/></param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<DayType> FilterByIsActive(this IQueryable<DayType> query, bool? isActive) =>
		isActive.HasValue ? query.Where(x => x.IsActive.Equals(isActive)) : query;
}
