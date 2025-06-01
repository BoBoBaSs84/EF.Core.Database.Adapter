using BB84.Home.Application.Services.Identity;

using FluentAssertions;

namespace ApplicationTests.Services.Identity;

public sealed partial class TokenServiceTests
{
	[TestMethod]
	[TestCategory(nameof(TokenService.GenerateAccessToken))]
	public void GenerateAccessTokenShouldGenerateAccessToken()
	{
		TokenService sut = CreateMockedInstance();

		(string accessToken, DateTimeOffset accessTokenExpiration) = sut.GenerateAccessToken([]);

		accessToken.Should().NotBeNull();
		accessTokenExpiration.Should().NotBe(DateTimeOffset.MaxValue);
	}
}
