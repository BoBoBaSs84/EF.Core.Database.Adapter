using System.ComponentModel.DataAnnotations;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BB84.Home.Connector.Options;

/// <summary>
/// Represents the settings for the home API client.
/// </summary>
public sealed class HomeApiOption : IEquatable<HomeApiOption>
{
	private const string OptionName = "HomeApi";

	/// <summary>
	/// Gets or sets the base address of the home API.
	/// </summary>
	[Required]
	public required string BaseAddress { get; init; }

	/// <summary>
	/// Gets or sets the timeout duration in seconds for home API requests.
	/// </summary>
	[Required, Range(10, 300)]
	public int Timeout { get; init; }

	/// <summary>
	/// Binds the <see cref="HomeApiOption"/> to the configuration section and registers it in the service collection.
	/// </summary>
	/// <param name="services">The service collection to register the options in.</param>
	/// <returns>The same options builder instance, so that multiple calls can be chained.</returns>
	public static OptionsBuilder<HomeApiOption> Bind(IServiceCollection services)
	{
		return services.AddOptions<HomeApiOption>()
			.BindConfiguration(OptionName)
			.ValidateDataAnnotations()
			.ValidateOnStart();
	}

	/// <inheritdoc/>
	public bool Equals(HomeApiOption? other)
		=> other is not null && this is not null && BaseAddress == other.BaseAddress && Timeout == other.Timeout;

	/// <inheritdoc/>
	public override bool Equals(object? obj)
		=> Equals(obj as HomeApiOption);

	/// <inheritdoc/>
	public override int GetHashCode()
		=> base.GetHashCode();
}
