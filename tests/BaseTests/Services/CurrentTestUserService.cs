using Application.Interfaces.Presentation.Services;

namespace BaseTests.Services;

internal sealed class CurrentTestUserService : ICurrentUserService
{
	public Guid UserId { get; set; }
	public string UserName { get; set; }
}
