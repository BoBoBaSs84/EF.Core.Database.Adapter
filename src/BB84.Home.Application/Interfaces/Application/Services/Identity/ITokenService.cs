using System.Security.Claims;

using BB84.Home.Domain.Entities.Identity;

using Microsoft.AspNetCore.Identity;

namespace BB84.Home.Application.Interfaces.Application.Services.Identity;

/// <summary>
/// The interface for the token service.
/// </summary>
public interface ITokenService
{
	/// <summary>
	/// Returns the identity from the provided <paramref name="token"/>.
	/// </summary>
	/// <param name="token">The token to use.</param>
	/// <returns>The identity.</returns>
	ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

	/// <summary>
	/// Remove the refresh token for a <paramref name="user"/>.
	/// </summary>
	/// <param name="user">The user to instance to use.</param>
	/// <returns>Whether a token was removed.</returns>
	Task<IdentityResult> RemoveRefreshTokenAsync(UserEntity user);

	/// <summary>
	/// Sets an refresh <paramref name="token"/> for a <paramref name="user"/>.
	/// </summary>
	/// <param name="user">The user to instance to use.</param>
	/// <param name="token">The token to set.</param>
	/// <returns>Whether the user was successfully updated.</returns>
	Task<IdentityResult> SetRefreshTokenAsync(UserEntity user, string token);

	/// <summary>
	/// Indicates whether the specified refresh <paramref name="token"/> is valid
	/// for the given <paramref name="user"/>.
	/// </summary>
	/// <param name="user">The user to instance to use.</param>
	/// <param name="token">The token to validate.</param>
	/// <returns>True if the token is valid, otherwise false.</returns>
	Task<bool> VerifyRefreshTokenAsync(UserEntity user, string token);

	/// <summary>
	/// Generates a new refresh token for the provided <paramref name="user"/>.
	/// </summary>
	/// <param name="user">The user to instance to use.</param>
	/// <returns>A new refresh token.</returns>
	Task<string> GenerateRefreshTokenAsync(UserEntity user);

	/// <summary>
	/// Generates a new access token with the provied <paramref name="claims"/>.
	/// </summary>
	/// <param name="claims">The claimes to use.</param>
	/// <returns>A new access token.</returns>
	string GenerateAccessToken(IEnumerable<Claim> claims);
}
