namespace BB84.Home.Application.Interfaces.Presentation.Services;

/// <summary>
/// The current user service interface.
/// </summary>
public interface ICurrentUserService
{
	/// <summary>
	/// The current user id.
	/// </summary>
	Guid UserId { get; }

	/// <summary>
	/// The current user name.
	/// </summary>
	string UserName { get; }
}
