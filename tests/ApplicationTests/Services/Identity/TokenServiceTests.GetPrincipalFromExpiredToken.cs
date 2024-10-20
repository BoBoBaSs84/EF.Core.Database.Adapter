using System.Security.Claims;

using Application.Services.Identity;

using BaseTests.Helpers;

using FluentAssertions;

namespace ApplicationTests.Services.Identity;

public sealed partial class TokenServiceTests
{
	[TestMethod]
	[TestCategory(nameof(TokenService.GetPrincipalFromExpiredToken))]
	public void GetPrincipalFromExpiredTokenShouldReturnPrincipalWhenTokenValid()
	{
		TokenService sut = CreateMockedInstance();
		string token = sut.GenerateAccessToken([]);

		ClaimsPrincipal principal = sut.GetPrincipalFromExpiredToken(token);

		AssertionHelper.AssertInScope(() =>
		{
			principal.Should().NotBeNull();
			principal.Identity.Should().NotBeNull();
			principal.Identity!.IsAuthenticated.Should().BeTrue();
		});
	}
}
