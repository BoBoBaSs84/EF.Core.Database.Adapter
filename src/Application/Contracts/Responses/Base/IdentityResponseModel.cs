using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Contracts.Responses.Base;

/// <summary>
/// The <see langword="abstract"/> identity response model.
/// </summary>
public abstract class IdentityResponseModel
{
	/// <summary>
	/// The <see cref="Id"/> property.
	/// </summary>
	[Column(Order = 1)]
	public Guid Id { get; set; }
}
