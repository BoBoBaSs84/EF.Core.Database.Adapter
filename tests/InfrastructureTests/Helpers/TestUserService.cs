using BB84.Home.Application.Interfaces.Presentation.Services;

namespace InfrastructureTests.Helpers;

public sealed class TestUserService : ICurrentUserService
{
	public TestUserService()
	{
		UserId = Guid.NewGuid();
		UserName = "UnitTestUser";
	}

	public Guid UserId { get; }
	public string UserName { get; }
}
