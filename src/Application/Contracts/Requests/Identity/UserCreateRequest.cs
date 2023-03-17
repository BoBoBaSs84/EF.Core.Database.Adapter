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
	[Required, MaxLength(100)]
	public string FirstName { get; set; } = default!;

	/// <summary>
	/// The middle name of the user.
	/// </summary>
	[MaxLength(100)]
	public string? MiddleName { get; set; } = default!;

	/// <summary>
	/// The last name of the user.
	/// </summary>
	[Required, MaxLength(100)]
	public string LastName { get; set; } = default!;

	/// <summary>
	/// The date of birth of the user.
	/// </summary>
	[DataType(DataType.Date)]
	public DateTime? DateOfBirth { get; set; } = default!;

	/// <summary>
	/// The user name of the user.
	/// </summary>
	[Required]
	public string UserName { get; set; } = default!;

	/// <summary>
	/// The email of the user.
	/// </summary>
	[Required, EmailAddress, DataType(DataType.EmailAddress)]
	public string Email { get; set; } = default!;

	/// <summary>
	/// The password of the user.
	/// </summary>
	[Required, DataType(DataType.Password)]
	public string Password { get; set; } = default!;

	/// <summary>
	/// The roles of the user.
	/// </summary>
	public ICollection<string> Roles { get; set; } = default!;
}
