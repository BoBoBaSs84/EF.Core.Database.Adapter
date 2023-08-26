using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Common.EntityBaseTypes.Interfaces;

namespace Domain.Common.EntityBaseTypes;

/// <summary>
/// The abstract identity model class.
/// </summary>
/// <remarks>
/// Implements the following interface members:
/// <list type="bullet">
/// <item>The <see cref="IIdentity{TKey}"/> interface</item>
/// <item>The <see cref="IConcurrency"/> interface</item>
/// </list>
/// </remarks>
public abstract class IdentityModel : IIdentity<int>, IConcurrency
{
	/// <inheritdoc/>
	[Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; private set; } = default!;

	/// <inheritdoc/>
	[Timestamp, Column(Order = 2), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
	public byte[] Timestamp { get; private set; } = default!;
}
