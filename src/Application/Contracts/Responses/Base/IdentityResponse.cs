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
	[Column(Order = 1)]
	public Guid Id { get; set; }
}
