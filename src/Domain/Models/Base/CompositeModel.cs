using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Interfaces.Models;

namespace Domain.Models.Base;

/// <summary>
/// The <see langword="abstract"/> composite model class.
/// </summary>
/// <remarks>
/// Implements the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IConcurrencyModel"/> interface</item>
/// <item>The <see cref="IAuditedModel"/> interface</item>
/// </list>
/// </remarks>
public abstract class CompositeModel : IConcurrencyModel, IAuditedModel
{
	[Timestamp, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
	public byte[] Timestamp { get; private set; }

	[Column(Order = 2)]
	public Guid CreatedBy { get; set; }

	[Column(Order = 3)]
	public Guid? ModifiedBy { get; set; }
}
