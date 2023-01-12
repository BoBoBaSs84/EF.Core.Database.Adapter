using Database.Adapter.Entities.BaseTypes.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using static Database.Adapter.Entities.Constants.XmlConstants;

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
[XmlRoot(Namespace = XmlNameSpaces.AUDITED_NAMESPACE)]
public abstract class AuditedModel : IdentityModel, IAuditedModel
{
	/// <inheritdoc/>
	[Column(Order = 3)]
	[XmlElement(DataType = XmlDataType.INT, ElementName = nameof(CreatedBy))]
	public int CreatedBy { get; set; } = default;
	/// <inheritdoc/>
	[Column(Order = 4)]
	[XmlElement(DataType = XmlDataType.INT, ElementName = nameof(ModifiedBy))]
	public int? ModifiedBy { get; set; } = default!;
	/// <inheritdoc/>
	public bool ShouldSerializeModifiedBy() => ModifiedBy is not null;
}
