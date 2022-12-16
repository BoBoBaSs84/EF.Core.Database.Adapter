using Database.Adapter.Entities.BaseTypes.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Entities.BaseTypes;

/// <summary>
/// The abstract enumerator model base class.
/// </summary>
/// <remarks>
/// Implements the following interface members:
/// <list type="bullet">
/// <item>The <see cref="IEnumeratorModel"/> interface</item>
/// </list>
/// </remarks>
public abstract class EnumeratorModel : IEnumeratorModel
{
	/// <inheritdoc/>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	/// <inheritdoc/>
	[Required(AllowEmptyStrings = false)]
	[StringLength(SqlStringLength.MAX_LENGHT_128)]
	public string Name { get; set; } = default!;
	/// <inheritdoc/>
	[StringLength(SqlStringLength.MAX_LENGHT_512)]
	public string? Description { get; set; } = default!;
}
