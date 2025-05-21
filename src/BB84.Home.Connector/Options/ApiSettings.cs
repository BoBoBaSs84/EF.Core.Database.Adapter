namespace BB84.Home.Connector.Options;

/// <summary>
/// Represents the settings for the API client.
/// </summary>
public sealed class ApiSettings
{
	/// <summary>
	/// The base address of the API.
	/// </summary>
	public required string BaseAddress { get; init; }

	/// <summary>
	/// The timeout for the API requests in seconds.
	/// </summary>
	public int Timeout { get; init; }

	/// <summary>
	/// The authentication token for the API.
	/// </summary>
	public string? Token { get; set; }

	/// <summary>
	/// The refresh token for the API.
	/// </summary>
	public string? RefreshToken { get; set; }
}
