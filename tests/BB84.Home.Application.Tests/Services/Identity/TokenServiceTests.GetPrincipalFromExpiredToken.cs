using System.Security.Claims;

using BB84.Home.Application.Services.Identity;
using BB84.Home.Base.Tests.Helpers;

using FluentAssertions;

namespace ApplicationTests.Services.Identity;

public sealed partial class TokenServiceTests
{
	[TestMethod]
	[TestCategory(nameof(TokenService.GetPrincipalFromExpiredToken))]
	public void GetPrincipalFromExpiredTokenShouldReturnPrincipalWhenTokenValid()
	{
		TokenService sut = CreateMockedInstance();
		_dateTimeServiceMock.Setup(x=>x.UtcNow)
			.Returns(DateTime.UtcNow);
		_dateTimeServiceMock.Setup(x=>x.Now)
			.Returns(DateTime.Now);
		(string accessToken, DateTimeOffset accessTokenExpiration) = sut.GenerateAccessToken([]);

		ClaimsPrincipal principal = sut.GetPrincipalFromExpiredToken(accessToken);

		AssertionHelper.AssertInScope(() =>
		{
			principal.Should().NotBeNull();
			principal.Identity.Should().NotBeNull();
			principal.Identity!.IsAuthenticated.Should().BeTrue();
		});
	}
}
