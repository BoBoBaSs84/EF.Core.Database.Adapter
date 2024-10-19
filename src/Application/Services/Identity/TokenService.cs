using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Application.Interfaces.Application.Common;
using Application.Interfaces.Application.Identity;
using Application.Options;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Identity;

/// <summary>
/// The token service class.
/// </summary>
/// <param name="options">The bearer option instance to use.</param>
/// <param name="dateTimeService">The date time service instance to use.</param>
internal sealed class TokenService(IOptions<BearerSettings> options, IDateTimeService dateTimeService) : ITokenService
{
	private readonly BearerSettings _bearerSettings = options.Value;

	public string GenerateAccessToken(IEnumerable<Claim> claims)
	{
		SigningCredentials signingCredentials = GetSigningCredentials();
		JwtSecurityToken tokenOptions = GetSecurityToken(signingCredentials, claims);
		return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
	}

	public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
	{
		TokenValidationParameters validationParameters = new()
		{
			ValidateAudience = true,
			ValidateIssuer = true,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = GetSecurityKey(),
			ValidateLifetime = false,
		};

		JwtSecurityTokenHandler tokenHandler = new();
		ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);

		return securityToken is not JwtSecurityToken jwtSecurityToken
			? throw new SecurityTokenException("Invalid token.")
			: jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.OrdinalIgnoreCase)
				? principal
				: throw new SecurityTokenException("Invalid token.");
	}

	private SigningCredentials GetSigningCredentials()
	{
		SymmetricSecurityKey secret = GetSecurityKey();
		return new SigningCredentials(secret, SecurityAlgorithms.HmacSha512);
	}

	private SymmetricSecurityKey GetSecurityKey()
	{
		byte[] key = Encoding.UTF8.GetBytes(_bearerSettings.SecurityKey);
		return new SymmetricSecurityKey(key);
	}

	private JwtSecurityToken GetSecurityToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
	{
		JwtSecurityToken token = new(
			issuer: _bearerSettings.Issuer,
			audience: _bearerSettings.Audience,
			claims: claims,
			expires: dateTimeService.Now.AddMinutes(_bearerSettings.ExpiryInMinutes),
			signingCredentials: signingCredentials
			);

		return token;
	}
}
