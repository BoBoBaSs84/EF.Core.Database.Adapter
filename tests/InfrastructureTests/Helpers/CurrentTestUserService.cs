using Application.Interfaces.Infrastructure.Identity;

namespace InfrastructureTests.Helpers;

internal class CurrentTestUserService : ICurrentUserService
{
	public int UserId => 1;
}
