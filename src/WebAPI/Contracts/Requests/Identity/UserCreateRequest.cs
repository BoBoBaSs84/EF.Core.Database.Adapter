using System.ComponentModel.DataAnnotations;

namespace WebAPI.Contracts.Requests.Identity;

public sealed class UserCreateRequest
{
	[Required, MaxLength(100)]
	public string FirstName { get; set; }

	[Required, MaxLength(100)]
	public string LastName { get; set; }

	[Required]
	public string UserName { get; set; }

	[Required, DataType(DataType.Password)]
	public string Password { get; set; }

	[Required, EmailAddress, DataType(DataType.EmailAddress)]
	public string Email { get; set; }

	public ICollection<string> Roles { get; set; }

}
