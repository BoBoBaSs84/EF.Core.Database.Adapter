namespace Application.Features.Requests.Base;

/// <summary>
/// The base request parameter class.
/// </summary>
public abstract class RequestParameters
{
	private const int MinPageNumber = 1;
	private const int MaxPageSize = 100;
	private int _pageNumber = 1;
	private int _pageSize = 10;

	/// <summary>
	/// The page number property.
	/// </summary>
	public int PageNumber { get; set; } = 1;

	/// <summary>
	/// The desired page size.
	/// </summary>
	/// <remarks>
	/// Should be between 10 and 100. The default value is 10.
	/// </remarks>
	public int PageSize { get; set; } = 10;
	
}
