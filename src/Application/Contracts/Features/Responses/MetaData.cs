namespace Application.Contracts.Features.Responses;

/// <summary>
/// The meta data class.
/// </summary>
public sealed class MetaData
{
	/// <summary>
	/// The current page property.
	/// </summary>
	public int CurrentPage { get; private set; }
	/// <summary>
	/// The total pages property.
	/// </summary>
	public int TotalPages { get; private set; }
	/// <summary>
	/// The page size property.
	/// </summary>
	public int PageSize { get; private set; }
	/// <summary>
	/// The total count property.
	/// </summary>
	public int TotalCount { get; private set; }
	/// <summary>
	/// Is there a previous page available?
	/// </summary>
	public bool HasPrevious => CurrentPage > 1;
	/// <summary>
	/// Is there a next page available?
	/// </summary>
	public bool HasNext => CurrentPage < TotalPages;

	/// <summary>
	/// Initilizes an instance of <see cref="MetaData"/> class.
	/// </summary>
	/// <param name="currentPage">The current page.</param>
	/// <param name="totalPages">The total pages.</param>
	/// <param name="pageSize">The page size.</param>
	/// <param name="totalCount">The total count.</param>
	public MetaData(int currentPage, int totalPages, int pageSize, int totalCount)
	{
		CurrentPage = currentPage;
		TotalPages = totalPages;
		PageSize = pageSize;
		TotalCount = totalCount;
	}
}
