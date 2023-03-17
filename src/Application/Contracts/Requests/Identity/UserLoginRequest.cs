using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Requests.Identity;

public sealed class UserLoginRequest
{
	[Required]
	public string UserName { get; set; }

	[Required, DataType(DataType.Password)]
	public string Password { get; set; }
}
