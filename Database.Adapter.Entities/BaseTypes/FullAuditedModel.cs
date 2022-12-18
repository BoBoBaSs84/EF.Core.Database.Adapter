using Database.Adapter.Entities.BaseTypes.Interfaces;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Database.Adapter.Entities.BaseTypes;

/// <summary>
/// The abstract full audited model class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="AuditedModel"/> class and implements the following interface members:
/// <list type="bullet">
/// <item>The <see cref="IActivatableModel"/> interface</item>
/// </list>
/// </remarks>
public abstract class FullAuditedModel : AuditedModel, IActivatableModel
{
	/// <inheritdoc/>
	[JsonPropertyName(nameof(IsActive))]
	[XmlAttribute(AttributeName = nameof(IsActive))]
	public bool? IsActive { get; set; } = default!;
	/// <inheritdoc/>
	public bool ShouldSerializeIsActive() => IsActive is not null;
}
