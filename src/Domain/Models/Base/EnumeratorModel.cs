using Domain.Interfaces.Models;

namespace Domain.Models.Base;

/// <summary>
/// The enumerator model class.
/// </summary>
public abstract class EnumeratorModel : IEnumeratorModel
{
	/// <inheritdoc/>
	public int Id { get; set; } = default!;

	/// <inheritdoc/>
	public string Name { get; set; } = default!;

	/// <inheritdoc/>
	public string Description { get; set; } = default!;

	/// <inheritdoc/>
	public bool IsDeleted { get; set; } = default!;
}
