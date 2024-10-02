using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Requests.Identity.Base;

/// <summary>
/// The user base request class.
/// </summary>
public abstract class UserBaseRequest
{
	/// <summary>
	/// The first name of the user.
	/// </summary>
	[Required, MaxLength(100)]
	public required string FirstName { get; init; }

	/// <summary>
	/// The last name of the user.
	/// </summary>
	[Required, MaxLength(100)]
	public required string LastName { get; init; }

	/// <summary>
	/// The email of the user.
	/// </summary>
	[Required, EmailAddress]
	public required string Email { get; init; }
}
