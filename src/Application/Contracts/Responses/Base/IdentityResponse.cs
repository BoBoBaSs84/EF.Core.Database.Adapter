using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Contracts.Responses.Base;

/// <summary>
/// The <see langword="abstract"/> identity response class.
/// </summary>
public abstract class IdentityResponse
{
	/// <summary>
	/// The globally unique identifier property.
	/// </summary>
	[DataType(DataType.Text)]
	public Guid Id { get; set; }
}
