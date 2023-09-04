using Application.Interfaces.Presentation.Services;

namespace ApplicationTests.Helpers;

internal sealed class CurrentTestUserService : ICurrentUserService
{
	public Guid UserId => Guid.NewGuid();

	public string UserName => "TestUser";
}
