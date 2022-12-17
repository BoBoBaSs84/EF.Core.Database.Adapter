using Database.Adapter.Entities.BaseTypes.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

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
public abstract class IdentityModel : IIdentityModel, IConcurrencyModel
{
	/// <inheritdoc/>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[JsonPropertyName(nameof(Id))]
	[XmlAttribute(AttributeName = nameof(Id))]
	public Guid Id { get; set; } = default!;
	/// <inheritdoc/>
	[Timestamp]
	[JsonPropertyName(nameof(Timestamp))]
	[XmlAttribute(AttributeName = nameof(Timestamp))]
	public byte[] Timestamp { get; set; } = default!;
	/// <inheritdoc/>
	public bool ShouldSerializeTimestamp() => false;
}
