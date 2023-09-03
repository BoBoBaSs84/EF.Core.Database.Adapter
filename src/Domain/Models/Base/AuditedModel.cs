using System.ComponentModel.DataAnnotations.Schema;

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
	[Column(Order = 3)]
	public Guid CreatedBy { get; set; }

	/// <inheritdoc/>
	[Column(Order = 4)]
	public Guid? ModifiedBy { get; set; }
}
