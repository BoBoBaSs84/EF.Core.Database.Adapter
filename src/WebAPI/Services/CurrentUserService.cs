using Application.Interfaces.Infrastructure.Identity;

namespace WebAPI.Services;

public sealed class CurrentUserService : ICurrentUserService
{
	private readonly IHttpContextAccessor _contextAccessor;

	public CurrentUserService(IHttpContextAccessor contextAccessor)
	{
		_contextAccessor = contextAccessor;
	}

	// HACK: for now (>.<)
	public int UserId => 1;
}
