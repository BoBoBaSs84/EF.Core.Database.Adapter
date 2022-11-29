using Database.Adapter.Entities.BaseTypes.Interfaces;
using System.ComponentModel.DataAnnotations;

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
	[Required]
	public Guid CreatedBy { get; set; } = default!;
	/// <inheritdoc/>
	public Guid? ModifiedBy { get; set; } = default!;
}
