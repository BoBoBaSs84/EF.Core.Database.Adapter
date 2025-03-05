using System.ComponentModel.DataAnnotations;

namespace BB84.Home.Application.Contracts.Responses.Base;

/// <summary>
/// The <see langword="abstract"/> identity response class.
/// </summary>
public abstract class IdentityResponse
{
	/// <summary>
	/// The globally unique identifier property.
	/// </summary>	
	[Required]
	public Guid Id { get; set; }
}
