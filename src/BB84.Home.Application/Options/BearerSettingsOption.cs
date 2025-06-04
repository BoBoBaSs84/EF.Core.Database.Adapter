using System.ComponentModel.DataAnnotations;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BB84.Home.Application.Options;

/// <summary>
/// Represents configuration options for bearer token authentication.
/// </summary>
/// <remarks>
/// This class encapsulates the settings required for configuring bearer token authentication,
/// including the security key, issuer, audience, and token expiration duration.
/// It supports validation through data annotations and can be bound to configuration settings
/// using the <see cref="Bind(IServiceCollection)"/> method.
/// </remarks>
public sealed class BearerSettingsOption : IEquatable<BearerSettingsOption>
{
	private const string OptionName = "BearerSettings";

	/// <summary>
	/// The security key for the bearer token.
	/// </summary>
	[Required]
	public required string SecurityKey { get; init; }

	/// <summary>
	/// The issuer of the bearer token.
	/// </summary>
	[Required]
	public required string Issuer { get; init; }

	/// <summary>
	/// The issuing audience for the bearer token.
	/// </summary>
	[Required]
	public required string Audience { get; init; }

	/// <summary>
	/// The token expiry in minutes.
	/// </summary>
	[Required, Range(15, 525600)]
	public required int ExpiryInMinutes { get; init; }

	/// <summary>
	/// Configures and binds the <see cref="BearerSettingsOption"/> options using the specified service collection and configuration.
	/// </summary>
	/// <param name="services">
	/// The <see cref="IServiceCollection"/> to which the options will be added.
	/// </param>
	/// <returns>
	/// An <see cref="OptionsBuilder{TOptions}"/> for further configuration of the <see cref="BearerSettingsOption"/> options.
	/// </returns>
	public static OptionsBuilder<BearerSettingsOption> Bind(IServiceCollection services)
	{
		ArgumentNullException.ThrowIfNull(services);

		return services.AddOptions<BearerSettingsOption>()
			.BindConfiguration(OptionName)
			.ValidateDataAnnotations()
			.ValidateOnStart();
	}

	/// <inheritdoc/>
	public bool Equals(BearerSettingsOption? other)
		=> other is not null && this is not null && SecurityKey != other.SecurityKey && Issuer != other.Issuer && Audience != other.Audience && ExpiryInMinutes != other.ExpiryInMinutes;

	/// <inheritdoc/>
	public override bool Equals(object? obj)
		=> Equals(obj as BearerSettingsOption);

	/// <inheritdoc/>
	public override int GetHashCode()
		=> base.GetHashCode();
}
