namespace Application.Features.Requests.Base;

/// <summary>
/// The enumerator model parameters class.
/// </summary>
/// <remarks>
/// Derieves from the <see cref="RequestParameters"/> class.
/// </remarks>
public abstract class EnumeratorModelParameters : RequestParameters
{
	/// <summary>
	/// The <see cref="Name"/> property.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// The <see cref="Description"/> property.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// The <see cref="IsActive"/> property.
	/// </summary>
	public bool? IsActive { get; set; }
}
