using Application.Interfaces.Infrastructure.Services;

using Domain.Models.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

/// <summary>
/// The user service class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="UserManager{TUser}"/> class
/// and implements the members of the <see cref="IUserService"/>.
/// </remarks>
internal sealed class UserService : UserManager<UserModel>, IUserService
{
	/// <summary>
	/// Initializes a new instance of the user service class.
	/// </summary>
	/// <inheritdoc/>
	public UserService(IUserStore<UserModel> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<UserModel> passwordHasher,
		IEnumerable<IUserValidator<UserModel>> userValidators, IEnumerable<IPasswordValidator<UserModel>> passwordValidators,
		ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<UserModel>> logger)
		: base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
	{ }
}
