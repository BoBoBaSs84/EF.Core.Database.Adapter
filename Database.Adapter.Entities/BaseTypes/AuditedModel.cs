using Database.Adapter.Entities.BaseTypes.Interfaces;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Database.Adapter.Entities.BaseTypes;

/// <summary>
/// The abstract audited model class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="IdentityModel"/> class and implements the following interface members:
/// <list type="bullet">
/// <item>The <see cref="IAuditedModel"/> interface</item>
/// </list>
/// </remarks>
public abstract class AuditedModel : IdentityModel, IAuditedModel
{
	/// <inheritdoc/>
	[JsonPropertyName(nameof(CreatedBy))]
	[XmlElement(ElementName = nameof(CreatedBy))]
	public Guid CreatedBy { get; set; } = default;
	/// <inheritdoc/>
	[JsonPropertyName(nameof(ModifiedBy))]
	[XmlElement(ElementName = nameof(ModifiedBy))]
	public Guid? ModifiedBy { get; set; } = default!;
	/// <inheritdoc/>
	public bool ShouldSerializeModifiedBy() => ModifiedBy is not null;
}
