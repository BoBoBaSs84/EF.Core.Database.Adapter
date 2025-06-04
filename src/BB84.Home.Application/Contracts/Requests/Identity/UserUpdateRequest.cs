using System.ComponentModel.DataAnnotations;

using BB84.Home.Application.Contracts.Requests.Identity.Base;

namespace BB84.Home.Application.Contracts.Requests.Identity;

/// <summary>
/// The user update request class.
/// </summary>
public sealed class UserUpdateRequest : UserBaseRequest
{
	/// <summary>
	/// The middle name of the user.
	/// </summary>
	[MaxLength(100)]
	public string? MiddleName { get; set; }

	/// <summary>
	/// The date of birth of the user.
	/// </summary>	
	[DataType(DataType.Date)]
	public DateTime? DateOfBirth { get; set; }

	/// <summary>
	/// The phone number of the user.
	/// </summary>
	[Phone]
	public string? PhoneNumber { get; set; }

	/// <summary>
	/// The picture of the user.
	/// </summary>
	[DataType(DataType.Upload)]
	public byte[]? Picture { get; set; }

	/// <summary>
	/// The application preferences of the user.
	/// </summary>
	public PreferencesRequest? Preferences { get; set; }
}
