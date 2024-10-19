using System.Security.Claims;

namespace Application.Interfaces.Application.Identity;

/// <summary>
/// The interface for the token service.
/// </summary>
public interface ITokenService
{
	string GenerateAccessToken(IEnumerable<Claim> claims);
	ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
