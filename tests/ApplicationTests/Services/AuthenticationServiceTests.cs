using Domain.Entities.Identity;
using Application.Interfaces.Application;
using ApplicationTests.Helpers;

namespace ApplicationTests.Services;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class AuthenticationServiceTests : ApplicationBaseTests
{
	private IAuthenticationService _authenticationService = default!;

	[TestMethod]
	public async Task CreateUserAsyncTest()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();


		User user = EntityHelper.GetNewUser(true, true);

		var users = await _authenticationService.GetAll();
	}
}