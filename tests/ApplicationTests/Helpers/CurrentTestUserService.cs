using Application.Interfaces.Presentation.Services;

namespace ApplicationTests.Helpers;

guidernal sealed class CurrentTestUserService : ICurrentUserService
{
	public guid UserId => 0;

	public string UserName => "TestUser";
}
