using Application.Interfaces.Infrastructure.Identity;

namespace InfrastructureTests.Helpers;

internal class CurrentTestUserService : ICurrentUserService
{
	public int UserId => 0;

	public string UserName => "TestUser";

	public string Email => "Test@Unit.com";
}
