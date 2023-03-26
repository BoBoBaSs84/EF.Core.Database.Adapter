using Domain.Common.EntityBaseTypes.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SqlMaxLength = Domain.Constants.DomainConstants.Sql.MaxLength;

namespace Domain.Common.EntityBaseTypes;

/// <summary>
/// The abstract enumerator model class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityModel"/> class and implements the following interface members:
/// <list type="bullet">
/// <item>The <see cref="IIdentity{TKey}"/> interface</item>
/// <item>The <see cref="IConcurrency"/> interface</item>
/// </list>
/// </remarks>
public abstract class EnumeratorModel : IdentityModel, IActivatable, IEnumerator
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
	[MaxLength(SqlMaxLength.MAX_250)]
	public string Name { get; set; } = default!;

	/// <inheritdoc/>
	[MaxLength(SqlMaxLength.MAX_1000)]
	public string? Description { get; set; } = default!;

	/// <inheritdoc/>
	[DefaultValue(false)]
	public bool IsActive { get; set; } = default!;
}
