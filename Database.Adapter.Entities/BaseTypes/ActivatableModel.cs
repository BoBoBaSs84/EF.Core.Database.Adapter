using Database.Adapter.Entities.BaseTypes.Interfaces;
using System.Xml.Serialization;
using static Database.Adapter.Entities.Constants.XmlConstants;

namespace Database.Adapter.Entities.BaseTypes;

/// <summary>
/// The abstract activatable model class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="IdentityModel"/> class and implements the following interface members:
/// <list type="bullet">
/// <item>The <see cref="IActivatableModel"/> interface</item>
/// </list>
/// </remarks>
[XmlRoot(Namespace = XmlNameSpaces.ACTIVATABLE_NAMESPACE)]
public abstract class ActivatableModel : IdentityModel, IActivatableModel
{
	/// <inheritdoc/>
	[XmlAttribute(AttributeName = nameof(IsActive), DataType = XmlDataType.BOOL, Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
	public bool IsActive { get; set; } = default!;
	/// <inheritdoc/>
	public bool ShouldSerializeIsActive() => IsActive is false;
}
