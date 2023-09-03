using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Interfaces.Models;

namespace Domain.Models.Base;

/// <summary>
/// The <see langword="abstract"/> identity model class.
/// </summary>
/// <remarks>
/// Implements the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IIdentityModel"/> interface</item>
/// <item>The <see cref="IConcurrencyModel"/> interface</item>
/// </list>
/// </remarks>
public abstract class IdentityModel : IIdentityModel, IConcurrencyModel
{
	/// <inheritdoc/>
	[Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; private set; }

	/// <inheritdoc/>
	[Timestamp, Column(Order = 2), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
	public byte[] Timestamp { get; private set; } = default!;
}
