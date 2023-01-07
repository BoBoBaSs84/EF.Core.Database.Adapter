using Database.Adapter.Entities.BaseTypes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using static Database.Adapter.Entities.Constants.SqlConstants;
using static Database.Adapter.Entities.Constants.XmlConstants;

namespace Database.Adapter.Entities.BaseTypes;

/// <summary>
/// The abstract enumerator model base class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="ActivatableModel"/> class and implements the following interface members:
/// <list type="bullet">
/// <item>The <see cref="IEnumeratorModel"/> interface</item>
/// </list>
/// </remarks>
[Index(nameof(Name), IsUnique = true)]
[XmlRoot(Namespace = XmlNameSpaces.ENUMERATOR_NAMSPACE, IsNullable = false)]
public abstract class EnumeratorModel : IIdentityModel, IConcurrencyModel, IActivatableModel, IEnumeratorModel
{
	/// <inheritdoc/>
	[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
	[JsonPropertyName(nameof(Id))]
	[XmlAttribute(AttributeName = nameof(Id), DataType = XmlDataType.INT)]
	public int Id { get; set; } = default!;
	/// <inheritdoc/>
	/// <inheritdoc/>
	[Timestamp, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
	[JsonPropertyName(nameof(Timestamp))]
	[XmlElement(DataType = XmlDataType.BYTEARRAY, ElementName = nameof(Timestamp), Namespace = XmlNameSpaces.IDENTITY_NAMESPACE)]
	public byte[] Timestamp { get; set; } = default!;
	/// <inheritdoc/>
	[StringLength(SqlStringLength.MAX_LENGHT_128)]
	[JsonPropertyName(nameof(Name))]
	[XmlElement(DataType = XmlDataType.STRING, ElementName = nameof(Name), Namespace = XmlNameSpaces.ENUMERATOR_NAMSPACE)]
	public string Name { get; set; } = default!;
	/// <inheritdoc/>
	[StringLength(SqlStringLength.MAX_LENGHT_512)]
	[JsonPropertyName(nameof(Description))]
	[XmlElement(DataType = XmlDataType.STRING, ElementName = nameof(Description), Namespace = XmlNameSpaces.ENUMERATOR_NAMSPACE)]
	public string? Description { get; set; } = default!;
	/// <inheritdoc/>
	[JsonPropertyName(nameof(IsActive))]
	[XmlElement(DataType = XmlDataType.BOOL, ElementName = nameof(IsActive), Namespace = XmlNameSpaces.ACTIVATABLE_NAMESPACE)]
	public bool IsActive { get; set; } = default!;

	/// <inheritdoc/>
	public bool ShouldSerializeDescription() => Description is not null;
	/// <inheritdoc/>
	public bool ShouldSerializeIsActive() => IsActive is false;
	/// <inheritdoc/>
	public bool ShouldSerializeTimestamp() => false;
}
