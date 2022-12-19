using Database.Adapter.Entities.BaseTypes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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
[Index(nameof(Name), IsUnique = true), Index(nameof(Enumerator), IsUnique = true)]
[XmlRoot(Namespace = XmlNameSpaces.ENUMERATOR_NAMSPACE, IsNullable = false)]
public abstract class EnumeratorModel : ActivatableModel, IEnumeratorModel
{
	/// <inheritdoc/>
	[JsonPropertyName(nameof(Enumerator))]
	[XmlElement(DataType = XmlDataType.INT, ElementName = nameof(Enumerator))]
	public int Enumerator { get; set; } = default!;
	/// <inheritdoc/>
	[StringLength(SqlStringLength.MAX_LENGHT_128)]
	[JsonPropertyName(nameof(Name))]
	[XmlElement(DataType = XmlDataType.STRING, ElementName = nameof(Name))]
	public string Name { get; set; } = default!;
	/// <inheritdoc/>
	[StringLength(SqlStringLength.MAX_LENGHT_512)]
	[JsonPropertyName(nameof(Description))]
	[XmlElement(DataType = XmlDataType.STRING, ElementName = nameof(Description))]
	public string? Description { get; set; } = default!;
	/// <inheritdoc/>
	public bool ShouldSerializeDescription() => Description is not null;
}
