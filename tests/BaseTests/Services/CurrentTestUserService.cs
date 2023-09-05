using Application.Interfaces.Presentation.Services;

namespace BaseTests.Services;

internal sealed class CurrentTestUserService : ICurrentUserService
{
	public Guid UserId => Guid.NewGuid();
	public string UserName => "TestUser";
}
