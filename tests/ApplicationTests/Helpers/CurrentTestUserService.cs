using Application.Interfaces.Presentation.Services;

namespace ApplicationTests.Helpers;

internal sealed class CurrentTestUserService : ICurrentUserService
{
	public int UserId => 0;

	public string UserName => "TestUser";
}
