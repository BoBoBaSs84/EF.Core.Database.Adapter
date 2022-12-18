﻿using Database.Adapter.Entities.BaseTypes.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using System.Xml.Schema;
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
[XmlRoot(Namespace = XmlNameSpaces.IDENTITY_NAMESPACE, IsNullable = false)]
public abstract class IdentityModel : IIdentityModel, IConcurrencyModel
{
	/// <inheritdoc/>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[JsonPropertyName(nameof(Id))]
	[XmlElement(ElementName = nameof(Id))]
	public Guid Id { get; set; } = default!;
	/// <inheritdoc/>
	[Timestamp]
	[JsonPropertyName(nameof(Timestamp))]
	[XmlElement(DataType = XmlDataType.BYTEARRAY, ElementName = nameof(Timestamp))]
	public byte[] Timestamp { get; set; } = default!;
	/// <inheritdoc/>
	public bool ShouldSerializeTimestamp() => false;
}
