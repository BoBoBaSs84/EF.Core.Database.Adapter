using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Requests.Identity;

/// <summary>
/// The user login request class.
/// </summary>
public sealed class UserLoginRequest
{
	/// <summary>
	/// The user name of the user.
	/// </summary>
	[Required]
	public string UserName { get; set; } = default!;

	/// <summary>
	/// The password of the user.
	/// </summary>
	[Required, DataType(DataType.Password)]
	public string Password { get; set; } = default!;
}
