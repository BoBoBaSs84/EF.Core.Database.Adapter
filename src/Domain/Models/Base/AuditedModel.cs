using Domain.Interfaces.Models;

namespace Domain.Models.Base;

/// <summary>
/// The <see langword="abstract"/> audited model class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityModel"/> class
/// and implements the <see cref="IAuditedModel"/> interface.
/// </remarks>
public abstract class AuditedModel : IdentityModel, IAuditedModel
{
	/// <inheritdoc/>
	public Guid CreatedBy { get; set; }

	/// <inheritdoc/>
	public Guid? ModifiedBy { get; set; }
}
