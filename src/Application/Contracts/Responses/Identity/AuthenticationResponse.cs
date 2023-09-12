using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Responses.Identity;

/// <summary>
/// The authentication response class.
/// </summary>
public sealed class AuthenticationResponse
{
	/// <summary>
	/// The token property.
	/// </summary>
	[DataType(DataType.Text)]
	public string Token { get; set; } = string.Empty;

	/// <summary>
	/// The expiry date property.
	/// </summary>
	[DataType(DataType.DateTime)]
	public DateTime ExpiryDate { get; set; }
}
