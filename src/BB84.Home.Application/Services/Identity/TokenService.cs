using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using BB84.Home.Application.Interfaces.Application.Providers;
using BB84.Home.Application.Interfaces.Application.Services.Identity;
using BB84.Home.Application.Interfaces.Infrastructure.Services;
using BB84.Home.Application.Options;
using BB84.Home.Domain.Entities.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BB84.Home.Application.Services.Identity;

/// <summary>
/// The token service class.
/// </summary>
/// <param name="options">The bearer option instance to use.</param>
/// <param name="dateTimeService">The date time service instance to use.</param>
/// <param name="userService">The user service instance to use.</param>
internal sealed class TokenService(IOptions<BearerSettingsOption> options, IDateTimeProvider dateTimeService, IUserService userService) : ITokenService
{
	private const string RefreshTokenName = "RefreshToken";
	private readonly BearerSettingsOption _bearerSettings = options.Value;

	public (string AccessToken, DateTimeOffset AccessTokenExpiration) GenerateAccessToken(IEnumerable<Claim> claims)
	{
		SigningCredentials signingCredentials = GetSigningCredentials();
		JwtSecurityToken tokenOptions = GetSecurityToken(signingCredentials, claims);

		JwtSecurityTokenHandler tokenHandler = new();
		string token = tokenHandler.WriteToken(tokenOptions);

		return (token, DateTime.SpecifyKind(tokenOptions.ValidTo, DateTimeKind.Utc));
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

	public async Task<IdentityResult> SetRefreshTokenAsync(UserEntity user, string token)
	{
		IdentityResult result = await userService
			.SetAuthenticationTokenAsync(user, _bearerSettings.Issuer, RefreshTokenName, token)
			.ConfigureAwait(false);

		return result;
	}

	public async Task<string> GenerateRefreshTokenAsync(UserEntity user)
	{
		string token = await userService
			.GenerateUserTokenAsync(user, _bearerSettings.Issuer, RefreshTokenName)
			.ConfigureAwait(false);

		return token;
	}

	public async Task<IdentityResult> RemoveRefreshTokenAsync(UserEntity user)
	{
		IdentityResult result = await userService
			.RemoveAuthenticationTokenAsync(user, _bearerSettings.Issuer, RefreshTokenName)
			.ConfigureAwait(false);

		return result;
	}

	public async Task<bool> VerifyRefreshTokenAsync(UserEntity user, string token)
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
			expires: dateTimeService.UtcNow.AddMinutes(_bearerSettings.ExpiryInMinutes),
			signingCredentials: signingCredentials
			);

		return token;
	}
}
