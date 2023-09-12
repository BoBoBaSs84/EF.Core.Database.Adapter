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
	/// The middle name of the user.
	/// </summary>
	[MaxLength(100), DataType(DataType.Text)]
	public string? MiddleName { get; set; } = string.Empty;

	/// <summary>
	/// The last name of the user.
	/// </summary>
	[Required, MaxLength(100), DataType(DataType.Text)]
	public string LastName { get; set; } = string.Empty;

	/// <summary>
	/// The date of birth of the user.
	/// </summary>
	[DataType(DataType.Date)]
	public DateTime? DateOfBirth { get; set; }

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

	/// <summary>
	/// The picture of the user.
	/// </summary>
	public byte[]? Picture { get; set; }
}
