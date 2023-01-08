﻿using Database.Adapter.Entities.BaseTypes.Interfaces;
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
	[XmlElement(ElementName = nameof(CreatedBy))]
	public int CreatedBy { get; set; } = default;
	/// <inheritdoc/>
	[XmlElement(ElementName = nameof(ModifiedBy))]
	public int? ModifiedBy { get; set; } = default!;
	/// <inheritdoc/>
	public bool ShouldSerializeModifiedBy() => ModifiedBy is not null;
}
