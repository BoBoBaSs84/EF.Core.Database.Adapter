namespace Application.Features.Requests.Base;

/// <summary>
/// The base request parameter class.
/// </summary>
public abstract class RequestParameters
{
	private const int maxPageSize = 100;
	private int pageSize = 10;

	/// <summary>
	/// The page number property.
	/// </summary>
	public int PageNumber { get; set; } = 1;

	/// <summary>
	/// The page size property.
	/// </summary>
	/// <remarks>
	/// The current maximum is 100.
	/// </remarks>
	public int PageSize
	{
		get => pageSize;
		set => pageSize = value > maxPageSize ? maxPageSize : value;
	}
}
