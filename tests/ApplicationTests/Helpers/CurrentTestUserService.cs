using BB84.Home.Application.Interfaces.Presentation.Services;

namespace BB84.Home.Application.Tests.Helpers;

public sealed class CurrentTestUserService : ICurrentUserService
{
	public CurrentTestUserService()
	{
		UserId = Guid.NewGuid();
		UserName = "UnitTestUser";
	}

	public Guid UserId { get; }
	public string UserName { get; }
}
