using Domain.Common.EntityBaseTypes.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common.EntityBaseTypes;

/// <summary>
/// The abstract audited composite model class.
/// </summary>
/// Implements the following interface members:
/// <remarks>
/// <list type="bullet">
/// <item>The <see cref="IConcurrency"/> interface</item>
/// <item>The <see cref="IAudited"/> interface</item>
/// </list>
/// </remarks>
public abstract class AuditedCompositeModel : IConcurrency, IAudited
{
	/// <inheritdoc/>
	[Timestamp, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
	public byte[] Timestamp { get; private set; } = default!;

	/// <inheritdoc/>
	[Column(Order = 2)]
	public int CreatedBy { get; set; } = default;

	/// <inheritdoc/>
	[Column(Order = 3)]
	public int? ModifiedBy { get; set; } = default!;
}
