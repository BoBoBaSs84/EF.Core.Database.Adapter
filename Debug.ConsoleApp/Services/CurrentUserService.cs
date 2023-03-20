using Application.Interfaces.Infrastructure.Identity;

namespace Debug.ConsoleApp.Services;

public sealed class CurrentUserService : ICurrentUserService
{
	// HACK: for now (>.<)
	public int UserId => 1;

	public string? UserName => throw new NotImplementedException();
}
