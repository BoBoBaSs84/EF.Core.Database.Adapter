﻿using Domain.Common.EntityBaseTypes.Interfaces;

namespace Domain.Common.EntityBaseTypes;

/// <summary>
/// The abstract activatable model class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityModel"/> class and implements the following interface members:
/// <list type="bullet">
/// <item>The <see cref="IActivatableModel"/> interface</item>
/// </list>
/// </remarks>
public abstract class ActivatableModel : IdentityModel, IActivatableModel
{
	/// <inheritdoc/>
	public bool IsActive { get; set; } = default!;
}