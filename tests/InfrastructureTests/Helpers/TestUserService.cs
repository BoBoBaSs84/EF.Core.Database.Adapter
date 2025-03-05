using BB84.Home.Application.Interfaces.Presentation.Services;

namespace BB84.Home.Infrastructure.Tests.Helpers;

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
