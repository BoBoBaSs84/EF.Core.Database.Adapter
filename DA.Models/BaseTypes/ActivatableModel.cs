using DA.Models.BaseTypes.Interfaces;

namespace DA.Models.BaseTypes;

/// <summary>
/// The abstract activatable model class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="IdentityModel"/> class and implements the following interface members:
/// <list type="bullet">
/// <item>The <see cref="IActivatableModel"/> interface</item>
/// </list>
/// </remarks>
public abstract class ActivatableModel : IdentityModel, IActivatableModel
{
	/// <inheritdoc/>
	public bool IsActive { get; set; } = default!;
}
