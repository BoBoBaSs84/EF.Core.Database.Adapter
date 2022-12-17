using Database.Adapter.Entities.BaseTypes.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Entities.BaseTypes;

/// <summary>
/// The abstract enumerator model base class.
/// </summary>
/// <remarks>
/// Implements the following interface members:
/// <list type="bullet">
/// <item>The <see cref="IEnumeratorModel"/> interface</item>
/// </list>
/// </remarks>
public abstract class EnumeratorModel : IEnumeratorModel, IActivatableModel
{
	/// <inheritdoc/>	
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[JsonPropertyName(nameof(Id))]
	[XmlAttribute(AttributeName = nameof(Id))]
	public int Id { get; set; }
	/// <inheritdoc/>
	[StringLength(SqlStringLength.MAX_LENGHT_128)]
	[JsonPropertyName(nameof(Name))]
	[XmlAttribute(AttributeName = nameof(Name))]
	public string Name { get; set; } = default!;
	/// <inheritdoc/>
	[StringLength(SqlStringLength.MAX_LENGHT_512)]
	[JsonPropertyName(nameof(Description))]
	[XmlText]
	public string? Description { get; set; } = default!;
	/// <inheritdoc/>
	[JsonPropertyName(nameof(IsActive))]
	[XmlAttribute(AttributeName = nameof(IsActive))]
	public bool? IsActive { get; set; } = default!;
	/// <inheritdoc/>
	public bool ShouldSerializeDescription() => Description is not null;
	/// <inheritdoc/>
	public bool ShouldSerializeIsActive() => IsActive is not null;
}
