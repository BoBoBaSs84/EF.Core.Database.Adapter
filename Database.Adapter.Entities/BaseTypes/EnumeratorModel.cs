using Database.Adapter.Entities.BaseTypes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Database.Adapter.Entities.Constants.Sql;

namespace Database.Adapter.Entities.BaseTypes;

/// <summary>
/// The abstract enumerator model base class.
/// </summary>
/// <remarks>
/// Implements the following interface members:
/// <list type="bullet">
/// <item>The <see cref="IIdentityModel"/> interface</item>
/// <item>The <see cref="IConcurrencyModel"/> interface</item>
/// <item>The <see cref="IActivatableModel"/> interface</item>
/// <item>The <see cref="IEnumeratorModel"/> interface</item>
/// </list>
/// </remarks>
[Index(nameof(Name), IsUnique = true)]
public abstract class EnumeratorModel : IIdentityModel, IConcurrencyModel, IActivatableModel, IEnumeratorModel
{
	/// <inheritdoc/>
	[Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None)]
	public int Id { get; set; } = default!;
	/// <inheritdoc/>
	[Timestamp, Column(Order = 2), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
	public byte[] Timestamp { get; private set; } = default!;
	/// <inheritdoc/>
	[MaxLength(MaxLength.MAX_250)]
	public string Name { get; set; } = default!;
	/// <inheritdoc/>
	[MaxLength(MaxLength.MAX_1000)]
	public string? Description { get; set; } = default!;
	/// <inheritdoc/>
	public bool IsActive { get; set; } = default!;
}
