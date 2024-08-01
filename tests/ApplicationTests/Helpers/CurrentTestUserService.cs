using Application.Interfaces.Presentation.Services;

namespace ApplicationTests.Helpers;

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
