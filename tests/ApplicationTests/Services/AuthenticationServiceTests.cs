using Application.Interfaces.Infrastructure.Services;

using Domain.Models.Identity;

namespace ApplicationTests.Services;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, UnitTest.")]
public partial class AuthenticationServiceTests : ApplicationTestBase
{
	private readonly IAuthenticationService _authenticationService;
	private static RoleModel s_role = default!;
	private static UserModel s_user = default!;

	public AuthenticationServiceTests()
		=> _authenticationService = GetService<IAuthenticationService>();

	[ClassInitialize]
	public static void ClassInitialize(TestContext context)
	{
		s_role = DataSeedHelper.SeedTestRole();
		s_user = DataSeedHelper.SeedUser();
	}
}
