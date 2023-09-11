using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Application.Contracts.Responses.Base;

using Domain.Converters;

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
	public string FirstName { get; set; } = default!;

	/// <summary>
	/// The middle name of the user.
	/// </summary>
	[DataType(DataType.Text)]
	public string? MiddleName { get; set; }

	/// <summary>
	/// The last name of the user.
	/// </summary>
	[DataType(DataType.Text)]
	public string LastName { get; set; } = default!;

	/// <summary>
	/// The date of birth of the user.
	/// </summary>
	[JsonConverter(typeof(DateJsonConverter))]
	public DateTime? DateOfBirth { get; set; }

	/// <summary>
	/// The user name of the user.
	/// </summary>
	[DataType(DataType.Text)]
	public string UserName { get; set; } = default!;

	/// <summary>
	/// The email of the user.
	/// </summary>
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; } = default!;

	/// <summary>
	/// The phone number of the user.
	/// </summary>
	[DataType(DataType.PhoneNumber)]
	public string? PhoneNumber { get; set; }

	/// <summary>
	/// The picture of the user.
	/// </summary>
	public byte[]? Picture { get; set; }
}
