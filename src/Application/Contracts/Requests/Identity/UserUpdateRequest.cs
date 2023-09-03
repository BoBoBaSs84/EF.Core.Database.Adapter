using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Requests.Identity;

/// <summary>
/// The user update request class.
/// </summary>
public sealed class UserUpdateRequest
{
	/// <summary>
	/// The first name of the user.
	/// </summary>
	[Required, MaxLength(100), DataType(DataType.Text)]
	public string FirstName { get; set; } = default!;

	/// <summary>
	/// The middle name of the user.
	/// </summary>
	[MaxLength(100), DataType(DataType.Text)]
	public string? MiddleName { get; set; } = default!;

	/// <summary>
	/// The last name of the user.
	/// </summary>
	[Required, MaxLength(100), DataType(DataType.Text)]
	public string LastName { get; set; } = default!;

	/// <summary>
	/// The date of birth of the user.
	/// </summary>
	[DataType(DataType.Date)]
	public DateTime? DateOfBirth { get; set; } = default!;

	/// <summary>
	/// The email of the user.
	/// </summary>
	[Required, EmailAddress, DataType(DataType.EmailAddress)]
	public string Email { get; set; } = default!;

	/// <summary>
	/// The phone number of the user.
	/// </summary>
	[Phone, DataType(DataType.PhoneNumber)]
	public string? PhoneNumber { get; set; } = default!;

	/// <summary>
	/// The picture of the user.
	/// </summary>
	public byte[]? Picture { get; set; } = default!;
}
