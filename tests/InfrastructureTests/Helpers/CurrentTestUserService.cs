using Application.Common.Interfaces.Identity;

namespace InfrastructureTests.Helpers;

internal class CurrentTestUserService : ICurrentUserService
{
	public int UserId => 1;
}
