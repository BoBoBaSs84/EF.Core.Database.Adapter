using Application.Interfaces.Presentation.Services;

namespace BaseTests.Services;

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
