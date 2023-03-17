using Application.Interfaces.Infrastructure.Identity;

namespace InfrastructureTests.Helpers;

internal sealed class CurrentTestUserService : ICurrentUserService
{
	public int UserId => 1;
}
