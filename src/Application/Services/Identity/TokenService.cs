using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Application.Interfaces.Application.Common;
using Application.Interfaces.Application.Identity;
using Application.Interfaces.Infrastructure.Services;
using Application.Options;

using Domain.Models.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Identity;

/// <summary>
/// The token service class.
/// </summary>
/// <param name="options">The bearer option instance to use.</param>
/// <param name="dateTimeService">The date time service instance to use.</param>
/// <param name="userService">The user service instance to use.</param>
internal sealed class TokenService(IOptions<BearerSettings> options, IDateTimeService dateTimeService, IUserService userService) : ITokenService
{
	private const string RefreshTokenName = "RefreshToken";
	private readonly BearerSettings _bearerSettings = options.Value;

	public string GenerateAccessToken(IEnumerable<Claim> claims)
	{
		SigningCredentials signingCredentials = GetSigningCredentials();
		JwtSecurityToken tokenOptions = GetSecurityToken(signingCredentials, claims);

		JwtSecurityTokenHandler tokenHandler = new();
		string token = tokenHandler.WriteToken(tokenOptions);

		return token;
	}

	public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
	{
		TokenValidationParameters validationParameters = new()
		{
			ValidateAudience = true,
			ValidAudience = _bearerSettings.Audience,
			ValidateIssuer = true,
			ValidIssuer = _bearerSettings.Issuer,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = GetSecurityKey(),
			ValidateLifetime = false,
			ValidAlgorithms = [SecurityAlgorithms.HmacSha512],
		};

		JwtSecurityTokenHandler tokenHandler = new();
		ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);

		return securityToken is not JwtSecurityToken
			? throw new SecurityTokenException("Invalid token.")
			: principal;
	}

	public async Task<IdentityResult> SetRefreshTokenAsync(UserModel user, string token)
	{
		IdentityResult result = await userService
			.SetAuthenticationTokenAsync(user, _bearerSettings.Issuer, RefreshTokenName, token)
			.ConfigureAwait(false);

		return result;
	}

	public async Task<string> GenerateRefreshTokenAsync(UserModel user)
	{
		string token = await userService
			.GenerateUserTokenAsync(user, _bearerSettings.Issuer, RefreshTokenName)
			.ConfigureAwait(false);

		return token;
	}

	public async Task<IdentityResult> RemoveRefreshTokenAsync(UserModel user)
	{
		IdentityResult result = await userService
			.RemoveAuthenticationTokenAsync(user, _bearerSettings.Issuer, RefreshTokenName)
			.ConfigureAwait(false);

		return result;
	}

	public async Task<bool> VerifyRefreshTokenAsync(UserModel user, string token)
	{
		bool isValid = await userService
			.VerifyUserTokenAsync(user, _bearerSettings.Issuer, RefreshTokenName, token)
			.ConfigureAwait(false);

		return isValid;
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
