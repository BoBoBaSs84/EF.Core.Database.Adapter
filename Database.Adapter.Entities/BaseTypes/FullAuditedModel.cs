﻿using Database.Adapter.Entities.BaseTypes.Interfaces;
using System.ComponentModel;

namespace Database.Adapter.Entities.BaseTypes;

/// <summary>
/// The abstract full audited model class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="AuditedModel"/> class and implements the following interface members:
/// <list type="bullet">
/// <item>The <see cref="IActivatableModel"/> interface</item>
/// </list>
/// </remarks>
public abstract class FullAuditedModel : AuditedModel, IActivatableModel
{
	/// <inheritdoc/>
	[DefaultValue(true)]
	public bool IsActive { get; set; } = default!;
}
