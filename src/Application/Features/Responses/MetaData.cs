namespace Application.Features.Responses;

/// <summary>
/// The meta data class.
/// </summary>
/// <param name="currentPage">The current page.</param>
/// <param name="totalPages">The total pages.</param>
/// <param name="pageSize">The page size.</param>
/// <param name="totalCount">The total count.</param>
public sealed class MetaData(int currentPage, int totalPages, int pageSize, int totalCount)
{
	/// <summary>
	/// The current page property.
	/// </summary>
	public int CurrentPage { get; private set; } = currentPage;

	/// <summary>
	/// The total pages property.
	/// </summary>
	public int TotalPages { get; private set; } = totalPages;

	/// <summary>
	/// The page size property.
	/// </summary>
	public int PageSize { get; private set; } = pageSize;

	/// <summary>
	/// The total count property.
	/// </summary>
	public int TotalCount { get; private set; } = totalCount;

	/// <summary>
	/// Is there a previous page available?
	/// </summary>
	public bool HasPrevious => CurrentPage > 1;

	/// <summary>
	/// Is there a next page available?
	/// </summary>
	public bool HasNext => CurrentPage < TotalPages;
}
