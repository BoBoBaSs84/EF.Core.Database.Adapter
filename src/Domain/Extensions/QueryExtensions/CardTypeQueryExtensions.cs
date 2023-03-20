using Domain.Entities.Enumerator;

namespace Domain.Extensions.QueryExtensions;

/// <summary>
/// The card type query extensions class.
/// </summary>
public static class CardTypeQueryExtensions
{
	/// <summary>
	/// Should search the card type entities by the description.
	/// </summary>
	/// <param name="query">The query to search.</param>
	/// <param name="description">The description to be searched.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<CardType> SearchByDescription(this IQueryable<CardType> query, string? description) =>
		string.IsNullOrWhiteSpace(description) ? query : query.Where(x => x.Description!.Contains(description));

	/// <summary>
	/// Should search the card type entities by the name.
	/// </summary>
	/// <param name="query">The query to search.</param>
	/// <param name="name">The name to be searched.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<CardType> SearchByName(this IQueryable<CardType> query, string? name) =>
		string.IsNullOrWhiteSpace(name) ? query : query.Where(x => x.Name.Contains(name));

	/// <summary>
	/// Should filter the card type entities if their active.
	/// </summary>
	/// <param name="query">The query to search.</param>
	/// <param name="isActive"><see langword="true"/> or <see langword="false"/></param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<CardType> FilterByIsActive(this IQueryable<CardType> query, bool? isActive) =>
		isActive.HasValue ? query.Where(x => x.IsActive.Equals(isActive)) : query;
}
