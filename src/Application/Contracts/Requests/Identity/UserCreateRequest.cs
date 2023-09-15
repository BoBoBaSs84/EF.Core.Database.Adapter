using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Requests.Identity;

/// <summary>
/// The user create request class.
/// </summary>
public sealed class UserCreateRequest
{
	/// <summary>
	/// The first name of the user.
	/// </summary>
	[Required, MaxLength(100), DataType(DataType.Text)]
	public string FirstName { get; set; } = string.Empty;

	/// <summary>
	/// The last name of the user.
	/// </summary>
	[Required, MaxLength(100), DataType(DataType.Text)]
	public string LastName { get; set; } = string.Empty;

	/// <summary>
	/// The user name of the user.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public string UserName { get; set; } = string.Empty;

	/// <summary>
	/// The email of the user.
	/// </summary>
	[Required, EmailAddress, DataType(DataType.EmailAddress)]
	public string Email { get; set; } = string.Empty;

	/// <summary>
	/// The password of the user.
	/// </summary>
	[Required, DataType(DataType.Password)]
	public string Password { get; set; } = string.Empty;
}
