using Application.Common.Interfaces.Identity;

namespace InfrastructureTests;

internal class CurrentTestUserService : ICurrentUserService
{
	public int UserId => 1;
}
