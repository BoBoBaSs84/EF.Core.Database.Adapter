using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Application.Contracts.Responses.Base;

using Domain.Converters;
using Domain.Models.Common;

namespace Application.Contracts.Responses.Identity;

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
	[DataType(DataType.Text)]
	public string FirstName { get; set; } = string.Empty;

	/// <summary>
	/// The middle name of the user.
	/// </summary>
	[DataType(DataType.Text)]
	public string? MiddleName { get; set; }

	/// <summary>
	/// The last name of the user.
	/// </summary>
	[DataType(DataType.Text)]
	public string LastName { get; set; } = string.Empty;

	/// <summary>
	/// The date of birth of the user.
	/// </summary>
	[JsonConverter(typeof(DateJsonConverter))]
	public DateTime? DateOfBirth { get; set; }

	/// <summary>
	/// The user name of the user.
	/// </summary>
	[DataType(DataType.Text)]
	public string UserName { get; set; } = string.Empty;

	/// <summary>
	/// The email of the user.
	/// </summary>
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; } = string.Empty;

	/// <summary>
	/// The phone number of the user.
	/// </summary>
	[DataType(DataType.PhoneNumber)]
	public string? PhoneNumber { get; set; }

	/// <summary>
	/// The picture of the user.
	/// </summary>
	public string? Picture { get; set; }

	/// <summary>
	/// The application preferences of the user.
	/// </summary>
	public PreferencesModel? Preferences { get; set; }
}
