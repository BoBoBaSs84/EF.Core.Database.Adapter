using Database.Adapter.Entities.BaseTypes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using static Database.Adapter.Entities.Constants.SqlConstants;
using static Database.Adapter.Entities.Constants.XmlConstants;

namespace Database.Adapter.Entities.BaseTypes;

/// <summary>
/// The abstract enumerator model base class.
/// </summary>
/// <remarks>
/// Implements the following interface members:
/// <list type="bullet">
/// <item>The <see cref="IIdentityModel"/> interface</item>
/// <item>The <see cref="IConcurrencyModel"/> interface</item>
/// <item>The <see cref="IActivatableModel"/> interface</item>
/// <item>The <see cref="IEnumeratorModel"/> interface</item>
/// </list>
/// </remarks>
[Index(nameof(Name), IsUnique = true)]
[XmlRoot(Namespace = XmlNameSpaces.ENUMERATOR_NAMSPACE)]
public abstract class EnumeratorModel : IIdentityModel, IConcurrencyModel, IActivatableModel, IEnumeratorModel
{
	/// <inheritdoc/>
	[Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None)]
	[XmlAttribute(AttributeName = nameof(Id), DataType = XmlDataType.INT)]
	public int Id { get; set; } = default!;
	/// <inheritdoc/>
	[Timestamp, Column(Order = 2), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
	[XmlElement(DataType = XmlDataType.BYTEARRAY, ElementName = nameof(Timestamp), Namespace = XmlNameSpaces.IDENTITY_NAMESPACE)]
	public byte[] Timestamp { get; set; } = default!;
	/// <inheritdoc/>
	[StringLength(SqlStringLength.MAX_LENGHT_128)]
	[XmlElement(DataType = XmlDataType.STRING, ElementName = nameof(Name))]
	public string Name { get; set; } = default!;
	/// <inheritdoc/>
	[StringLength(SqlStringLength.MAX_LENGHT_512)]
	[XmlElement(DataType = XmlDataType.STRING, ElementName = nameof(Description))]
	public string? Description { get; set; } = default!;
	/// <inheritdoc/>
	[XmlElement(DataType = XmlDataType.BOOL, ElementName = nameof(IsActive), Namespace = XmlNameSpaces.ACTIVATABLE_NAMESPACE)]
	public bool IsActive { get; set; } = default!;
	/// <inheritdoc/>
	public bool ShouldSerializeDescription() => Description is not null;
	/// <inheritdoc/>
	public bool ShouldSerializeIsActive() => IsActive is false;
	/// <inheritdoc/>
	public bool ShouldSerializeTimestamp() => false;
}
