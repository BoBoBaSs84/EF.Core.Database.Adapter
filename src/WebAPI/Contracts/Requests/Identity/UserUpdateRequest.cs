using System.ComponentModel.DataAnnotations;

namespace WebAPI.Contracts.Requests.Identity;

public sealed class UserUpdateRequest
{
	[Required, MaxLength(100)]
	public string FirstName { get; set; }

	[MaxLength(100)]
	public string? MiddleName { get; set; }

	[Required, MaxLength(100)]
	public string LastName { get; set; }

	[DataType(DataType.Date)]
	public DateTime? DateOfBirth { get; set; } = default!;

	[Required, EmailAddress, DataType(DataType.EmailAddress)]
	public string Email { get; set; }

	[Required, DataType(DataType.Password)]
	public string Password { get; set; }
}
