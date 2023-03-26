using Domain.Common.EntityBaseTypes.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common.EntityBaseTypes;

/// <summary>
/// The abstract full audited model class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="AuditedModel"/> class and implements the following interface members:
/// <list type="bullet">
/// <item>The <see cref="ISoftDeleteable"/> interface</item>
/// </list>
/// </remarks>
public abstract class FullAuditedModel : AuditedModel, ISoftDeleteable
{
	/// <inheritdoc/>
	[Column(Order = 5), DefaultValue(false)]
	public bool IsDeleted { get; set; } = default!;
}
