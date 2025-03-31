namespace BB84.Home.Application.Features.Responses;

/// <summary>
/// The paged list class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="List{T}"/> class.
/// </remarks>
/// <typeparam name="T"></typeparam>
public sealed class PagedList<T> : List<T>, IPagedList<T> where T : class
{
	/// <inheritdoc/>
	public MetaData MetaData { get; set; }

	/// <summary>
	/// Initilizes an instance of <see cref="PagedList{T}"/> class.
	/// </summary>
	/// <param name="items">The enumerable interface of <typeparamref name="T"/>.</param>
	/// <param name="totalCount">The count of the list items.</param>
	/// <param name="pageNumber">The page number.</param>
	/// <param name="pageSize">The page size.</param>
	public PagedList(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
	{
		int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
		MetaData = new(pageNumber, totalPages, pageSize, totalCount);
		AddRange(items);
	}
}
