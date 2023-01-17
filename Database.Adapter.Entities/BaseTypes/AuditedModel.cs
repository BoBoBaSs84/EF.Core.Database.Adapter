using Database.Adapter.Entities.BaseTypes.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

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
	[Column(Order = 3)]
	public int CreatedBy { get; set; } = default;
	/// <inheritdoc/>
	[Column(Order = 4)]
	public int? ModifiedBy { get; set; } = default!;
}
