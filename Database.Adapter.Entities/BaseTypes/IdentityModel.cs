using Database.Adapter.Entities.BaseTypes.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using static Database.Adapter.Entities.Constants.XmlConstants;

namespace Database.Adapter.Entities.BaseTypes;

/// <summary>
/// The abstract identity model class.
/// </summary>
/// <remarks>
/// Implements the following interface members:
/// <list type="bullet">
/// <item>The <see cref="IIdentityModel"/> interface</item>
/// <item>The <see cref="IConcurrencyModel"/> interface</item>
/// </list>
/// </remarks>
[XmlRoot(Namespace = XmlNameSpaces.IDENTITY_NAMESPACE)]
public abstract class IdentityModel : IIdentityModel, IConcurrencyModel
{
	/// <inheritdoc/>
	[Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[XmlAttribute(AttributeName = nameof(Id))]
	public int Id { get; set; } = default!;
	/// <inheritdoc/>
	[Timestamp, Column(Order = 2), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
	[XmlElement(DataType = XmlDataType.BYTEARRAY, ElementName = nameof(Timestamp))]
	public byte[] Timestamp { get; set; } = default!;
	/// <inheritdoc/>
	public bool ShouldSerializeTimestamp() => false;
}
