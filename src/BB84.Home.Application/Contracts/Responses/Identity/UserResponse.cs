using System.ComponentModel.DataAnnotations;

using BB84.Home.Application.Contracts.Responses.Base;

namespace BB84.Home.Application.Contracts.Responses.Identity;

/// <summary>
/// The user response class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityResponse"/> class.
/// </remarks>
public sealed class UserResponse : IdentityResponse
{
	/// <summary>
	/// The first name of the user.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public required string FirstName { get; init; }

	/// <summary>
	/// The middle name of the user.
	/// </summary>	
	[DataType(DataType.Text)]
	public string? MiddleName { get; init; }

	/// <summary>
	/// The last name of the user.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public required string LastName { get; init; }

	/// <summary>
	/// The date of birth of the user.
	/// </summary>
	[DataType(DataType.Date)]
	public DateTime? DateOfBirth { get; init; }

	/// <summary>
	/// The user name of the user.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public required string UserName { get; init; }

	/// <summary>
	/// The email of the user.
	/// </summary>
	[Required, EmailAddress, DataType(DataType.EmailAddress)]
	public required string Email { get; init; }

	/// <summary>
	/// The phone number of the user.
	/// </summary>	
	[Phone, DataType(DataType.PhoneNumber)]
	public string? PhoneNumber { get; init; }

	/// <summary>
	/// The picture of the user.
	/// </summary>
	[DataType(DataType.Text)]
	public byte[]? Picture { get; init; }

	/// <summary>
	/// The application preferences of the user.
	/// </summary>
	public PreferencesResponse? Preferences { get; init; }
}
