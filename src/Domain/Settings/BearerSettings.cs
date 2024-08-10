#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Domain.Settings;

/// <summary>
/// The bearer settings class.
/// </summary>
public sealed class BearerSettings
{
	/// <summary>
	/// The security key for the bearer token.
	/// </summary>
	public string SecurityKey { get; init; }

	/// <summary>
	/// The issuer of the bearer token.
	/// </summary>
	public string Issuer { get; init; }

	/// <summary>
	/// The issuing audience for the bearer token.
	/// </summary>
	public string Audience { get; init; }

	/// <summary>
	/// The token expiry in minutes.
	/// </summary>
	public int ExpiryInMinutes { get; init; }
}
