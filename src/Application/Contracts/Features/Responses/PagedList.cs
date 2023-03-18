using Application.Contracts.Common;

namespace Application.Contracts.Features.Responses;

/// <summary>
/// The paged list class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="List{T}"/> class.
/// </remarks>
/// <typeparam name="T"></typeparam>
public sealed class PagedList<T> : List<T>, IPagedList<T> where T : BaseResponseModel
{
	/// <inheritdoc/>
	public MetaData MetaData { get; set; }

	/// <summary>
	/// Initilizes an instance of <see cref="PagedList{T}"/> class.
	/// </summary>
	/// <param name="items">The list items of <typeparamref name="T"/></param>
	/// <param name="count">The count of the list items.</param>
	/// <param name="pageNumber">The page number.</param>
	/// <param name="pageSize">The page size.</param>
	public PagedList(List<T> items, int count, int pageNumber, int pageSize)
	{
		MetaData = new(pageNumber, (int)Math.Ceiling(count / (double)pageSize), pageSize, count);
		AddRange(items);
	}
}
