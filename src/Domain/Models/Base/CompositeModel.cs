using Domain.Interfaces.Models;

namespace Domain.Models.Base;

/// <summary>
/// The <see langword="abstract"/> composite model class.
/// </summary>
/// <remarks>
/// Implements the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IConcurrencyModel"/> interface</item>
/// <item>The <see cref="IAuditedModel"/> interface</item>
/// </list>
/// </remarks>
public abstract class CompositeModel : IConcurrencyModel, IAuditedModel
{
	/// <inheritdoc/>
	public byte[] Timestamp { get; private set; } = default!;

	/// <inheritdoc/>
	public Guid CreatedBy { get; set; }

	/// <inheritdoc/>
	public Guid? ModifiedBy { get; set; }
}
