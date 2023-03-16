using System.ComponentModel.DataAnnotations;

namespace WebAPI.Contracts.Requests.Identity;

public sealed class UserLoginRequest
{
	[Required]
	public string UserName { get; set; }

	[Required, DataType(DataType.Password)]
	public string Password { get; set; }
}
