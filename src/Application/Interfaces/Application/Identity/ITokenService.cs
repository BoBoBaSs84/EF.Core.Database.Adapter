using System.Security.Claims;

using Domain.Models.Identity;

using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Application.Identity;

/// <summary>
/// The interface for the token service.
/// </summary>
public interface ITokenService
{
	ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
	Task<IdentityResult> RemoveRefreshTokenAsync(UserModel user);
	Task<IdentityResult> SetRefreshTokenAsync(UserModel user, string token);
	Task<bool> VerifyRefreshTokenAsync(UserModel user, string token);
	Task<string> GenerateRefreshTokenAsync(UserModel user);
	string GenerateAccessToken(IEnumerable<Claim> claims);
}
