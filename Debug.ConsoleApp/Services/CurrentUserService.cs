using Application.Common.Interfaces.Identity;

namespace Debug.ConsoleApp.Services;

public sealed class CurrentUserService : ICurrentUserService
{
	// HACK: for now (>.<)
	public int UserId => 1;
}
