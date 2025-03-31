namespace BB84.Home.Application.Features.Requests.Base;

/// <summary>
/// The base request parameter class.
/// </summary>
public abstract class RequestParameters
{
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
