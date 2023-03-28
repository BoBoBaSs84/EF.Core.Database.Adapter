using Application.Interfaces.Infrastructure.Services;

namespace ApplicationTests.Helpers;

internal sealed class CurrentTestUserService : ICurrentUserService
{
	public int UserId => 0;

	public string UserName => "TestUser";

	public string Email => "Test@Unit.com";
}
