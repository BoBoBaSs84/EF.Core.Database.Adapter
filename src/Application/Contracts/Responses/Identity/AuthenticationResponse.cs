using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Responses.Identity;

/// <summary>
/// The authentication response class.
/// </summary>
public sealed class AuthenticationResponse
{
	/// <summary>
	/// The <see cref="Token"/> property.
	/// </summary>
	[DataType(DataType.Text)]
	public string Token { get; set; } = default!;
}
