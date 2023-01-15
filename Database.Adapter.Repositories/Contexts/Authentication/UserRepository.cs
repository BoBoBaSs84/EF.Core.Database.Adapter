using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Contexts.Authentication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Repositories.Contexts.Authentication;

/// <summary>
/// The user repository class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="GenericRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IUserRepository"/> interface</item>
/// </list>
/// </remarks>
[SuppressMessage("Globalization", "CA1309", Justification = "Translation of the 'string.Equals' overload with a 'StringComparison' parameter is not supported.")]
internal sealed class UserRepository : GenericRepository<User>, IUserRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="UserRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public UserRepository(DbContext dbContext) : base(dbContext)
	{
	}
	/// <inheritdoc/>
	public User GetByEmail(string email, bool trackchanges = false) =>
		GetByCondition(
			expression: x => x.Email.Equals(email),
			trackChanges: trackchanges,
			includeProperties: new[] { nameof(User.UserRoles), $"{nameof(User.UserRoles)}.{nameof(UserRole.Role)}" }
			);
	/// <inheritdoc/>
	public User GetByUserName(string userName, bool trackchanges = false) =>
		GetByCondition(
			expression: x => x.UserName.Equals(userName),
			trackChanges: trackchanges,
			includeProperties: new[] { nameof(User.UserRoles), $"{nameof(User.UserRoles)}.{nameof(UserRole.Role)}" }
			);
}
