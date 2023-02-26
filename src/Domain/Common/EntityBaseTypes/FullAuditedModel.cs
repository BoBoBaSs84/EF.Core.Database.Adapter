using Domain.Common.EntityBaseTypes.Interfaces;

namespace Domain.Common.EntityBaseTypes;

/// <summary>
/// The abstract full audited model class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="AuditedModel"/> class and implements the following interface members:
/// <list type="bullet">
/// <item>The <see cref="IActivatableModel"/> interface</item>
/// </list>
/// </remarks>
public abstract class FullAuditedModel : AuditedModel, IActivatableModel
{
	/// <inheritdoc/>
	public bool IsActive { get; set; } = default!;
}
