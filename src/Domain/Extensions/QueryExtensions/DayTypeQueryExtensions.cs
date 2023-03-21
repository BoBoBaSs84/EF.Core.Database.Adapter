using Domain.Entities.Enumerator;

namespace Domain.Extensions.QueryExtensions;

/// <summary>
/// The day type query extensions class.
/// </summary>
public static class DayTypeQueryExtensions
{
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
