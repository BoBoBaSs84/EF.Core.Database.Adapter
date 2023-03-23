namespace Application.Contracts.Responses.Base;

/// <summary>
/// The base enumerator response model class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="ResponseModel"/> class.
/// </remarks>
public abstract class EnumeratorResponseModel : ResponseModel
{
	/// <summary>
	/// The <see cref="Name"/> property.
	/// </summary>
	public string Name { get; set; } = default!;

	/// <summary>
	/// The <see cref="Description"/> property.
	/// </summary>
	public string? Description { get; set; } = default!;

	/// <summary>
	/// The <see cref="IsActive"/> property.
	/// </summary>
	public bool IsActive { get; set; } = default!;
}
