using Domain.Constants;

using Microsoft.Extensions.Hosting;

namespace Infrastructure.Extensions;

/// <summary>
/// The host environment extensions class.
/// </summary>
internal static class HostEnvironmentExtensions
{
	/// <summary>
	/// Checks if the current host environment name is <see cref="DomainConstants.Environment.Testing"/>.
	/// </summary>
	/// <param name="hostEnvironment">An instance of <see cref="IHostEnvironment"/>.</param>
	/// <returns>True if the environment name is <see cref="DomainConstants.Environment.Testing"/>, otherwise false.</returns>
	public static bool IsTesting(this IHostEnvironment hostEnvironment)
		=> hostEnvironment.IsEnvironment(DomainConstants.Environment.Testing);
}
