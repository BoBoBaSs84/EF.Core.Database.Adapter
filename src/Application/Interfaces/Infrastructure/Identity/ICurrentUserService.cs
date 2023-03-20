namespace Application.Interfaces.Infrastructure.Identity;

/// <summary>
/// The current user service interface.
/// </summary>
public interface ICurrentUserService
{
	/// <summary>
	/// The current user id.
	/// </summary>
	int UserId { get; }

	/// <summary>
	/// The current user name.
	/// </summary>
	string? UserName { get; }
}
