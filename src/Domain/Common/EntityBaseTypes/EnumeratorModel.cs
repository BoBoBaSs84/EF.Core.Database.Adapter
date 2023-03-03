using Domain.Common.EntityBaseTypes.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.Constants.Sql;

namespace Domain.Common.EntityBaseTypes;

/// <summary>
/// The abstract enumerator model class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityModel"/> class and implements the following interface members:
/// <list type="bullet">
/// <item>The <see cref="IIdentityModel{TKey}"/> interface</item>
/// <item>The <see cref="IConcurrencyModel"/> interface</item>
/// </list>
/// </remarks>
//[Index(nameof(Name), IsUnique = true)]
// TODO: Should be done in the infrastructure layer ... for every enumerator!
public abstract class EnumeratorModel : IdentityModel, IActivatableModel, IEnumeratorModel
{
	/// <summary>
	/// The <see cref="Id"/> property.
	/// </summary>
	/// <remarks>
	/// This is the primary key of the database table.
	/// </remarks>
	[Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None)]
	new public int Id { get; set; } = default!;

	/// <inheritdoc/>
	[MaxLength(MaxLength.MAX_250)]
	public string Name { get; set; } = default!;

	/// <inheritdoc/>
	[MaxLength(MaxLength.MAX_1000)]
	public string? Description { get; set; } = default!;

	/// <inheritdoc/>
	public bool IsActive { get; set; } = default!;
}
