using DA.Models.BaseTypes.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Models.BaseTypes;

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
	[Column(Order = 3)]
	public int CreatedBy { get; private set; } = default;
	/// <inheritdoc/>
	[Column(Order = 4)]
	public int? ModifiedBy { get; private set; } = default!;
}
