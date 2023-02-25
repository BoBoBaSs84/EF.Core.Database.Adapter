using DA.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DA.Infrastructure.Application.Interfaces;

/// <summary>
/// The user service interface.
/// </summary>
/// <remarks>
/// The interface was generated from the <see cref="UserManager{User}"/> class.
/// </remarks>
public interface IUserService
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	Task<IdentityResult> AccessFailedAsync(User user);
	Task<IdentityResult> AddClaimAsync(User user, Claim claim);
	Task<IdentityResult> AddClaimsAsync(User user, IEnumerable<Claim> claims);
	Task<IdentityResult> AddLoginAsync(User user, UserLoginInfo login);
	Task<IdentityResult> AddPasswordAsync(User user, string password);
	Task<IdentityResult> AddToRoleAsync(User user, string role);
	Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles);
	Task<IdentityResult> ChangeEmailAsync(User user, string newEmail, string token);
	Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);
	Task<IdentityResult> ChangePhoneNumberAsync(User user, string phoneNumber, string token);
	Task<bool> CheckPasswordAsync(User user, string password);
	Task<IdentityResult> ConfirmEmailAsync(User user, string token);
	Task<int> CountRecoveryCodesAsync(User user);
	Task<IdentityResult> CreateAsync(User user);
	Task<IdentityResult> CreateAsync(User user, string password);
	Task<byte[]> CreateSecurityTokenAsync(User user);
	Task<IdentityResult> DeleteAsync(User user);
	Task<User> FindByEmailAsync(string email);
	Task<User> FindByIdAsync(string userId);
	Task<User> FindByLoginAsync(string loginProvider, string providerKey);
	Task<User> FindByNameAsync(string userName);
	Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail);
	Task<string> GenerateChangePhoneNumberTokenAsync(User user, string phoneNumber);
	Task<string> GenerateConcurrencyStampAsync(User user);
	Task<string> GenerateEmailConfirmationTokenAsync(User user);
	string GenerateNewAuthenticatorKey();
	Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(User user, int number);
	Task<string> GeneratePasswordResetTokenAsync(User user);
	Task<string> GenerateTwoFactorTokenAsync(User user, string tokenProvider);
	Task<string> GenerateUserTokenAsync(User user, string tokenProvider, string purpose);
	Task<int> GetAccessFailedCountAsync(User user);
	Task<string> GetAuthenticationTokenAsync(User user, string loginProvider, string tokenName);
	Task<string> GetAuthenticatorKeyAsync(User user);
	Task<IList<Claim>> GetClaimsAsync(User user);
	Task<string> GetEmailAsync(User user);
	Task<bool> GetLockoutEnabledAsync(User user);
	Task<DateTimeOffset?> GetLockoutEndDateAsync(User user);
	Task<IList<UserLoginInfo>> GetLoginsAsync(User user);
	Task<string> GetPhoneNumberAsync(User user);
	Task<IList<string>> GetRolesAsync(User user);
	Task<string> GetSecurityStampAsync(User user);
	Task<bool> GetTwoFactorEnabledAsync(User user);
	Task<User> GetUserAsync(ClaimsPrincipal principal);
	string GetUserId(ClaimsPrincipal principal);
	Task<string> GetUserIdAsync(User user);
	string GetUserName(ClaimsPrincipal principal);
	Task<string> GetUserNameAsync(User user);
	Task<IList<User>> GetUsersForClaimAsync(Claim claim);
	Task<IList<User>> GetUsersInRoleAsync(string roleName);
	Task<IList<string>> GetValidTwoFactorProvidersAsync(User user);
	Task<bool> HasPasswordAsync(User user);
	Task<bool> IsEmailConfirmedAsync(User user);
	Task<bool> IsInRoleAsync(User user, string role);
	Task<bool> IsLockedOutAsync(User user);
	Task<bool> IsPhoneNumberConfirmedAsync(User user);
	string NormalizeEmail(string email);
	string NormalizeName(string name);
	Task<IdentityResult> RedeemTwoFactorRecoveryCodeAsync(User user, string code);
	void RegisterTokenProvider(string providerName, IUserTwoFactorTokenProvider<User> provider);
	Task<IdentityResult> RemoveAuthenticationTokenAsync(User user, string loginProvider, string tokenName);
	Task<IdentityResult> RemoveClaimAsync(User user, Claim claim);
	Task<IdentityResult> RemoveClaimsAsync(User user, IEnumerable<Claim> claims);
	Task<IdentityResult> RemoveFromRoleAsync(User user, string role);
	Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles);
	Task<IdentityResult> RemoveLoginAsync(User user, string loginProvider, string providerKey);
	Task<IdentityResult> RemovePasswordAsync(User user);
	Task<IdentityResult> ReplaceClaimAsync(User user, Claim claim, Claim newClaim);
	Task<IdentityResult> ResetAccessFailedCountAsync(User user);
	Task<IdentityResult> ResetAuthenticatorKeyAsync(User user);
	Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
	Task<IdentityResult> SetAuthenticationTokenAsync(User user, string loginProvider, string tokenName, string tokenValue);
	Task<IdentityResult> SetEmailAsync(User user, string email);
	Task<IdentityResult> SetLockoutEnabledAsync(User user, bool enabled);
	Task<IdentityResult> SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd);
	Task<IdentityResult> SetPhoneNumberAsync(User user, string phoneNumber);
	Task<IdentityResult> SetTwoFactorEnabledAsync(User user, bool enabled);
	Task<IdentityResult> SetUserNameAsync(User user, string userName);
	Task<IdentityResult> UpdateAsync(User user);
	Task UpdateNormalizedEmailAsync(User user);
	Task UpdateNormalizedUserNameAsync(User user);
	Task<IdentityResult> UpdateSecurityStampAsync(User user);
	Task<bool> VerifyChangePhoneNumberTokenAsync(User user, string token, string phoneNumber);
	Task<bool> VerifyTwoFactorTokenAsync(User user, string tokenProvider, string token);
	Task<bool> VerifyUserTokenAsync(User user, string tokenProvider, string purpose, string token);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
