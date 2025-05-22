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
	public int Timeout { get; init; } = 30;
}
