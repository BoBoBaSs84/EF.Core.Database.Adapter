using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Contracts.Responses.Base;

/// <summary>
/// This is the base response model.
/// </summary>
/// <remarks>
/// Every response model should derive from it.
/// </remarks>
public abstract class ResponseModel
{
	/// <summary>
	/// The <see cref="Id"/> property.
	/// </summary>
	[Column(Order = 1), Range(1, int.MaxValue)]
	public int Id { get; set; } = default!;
}
