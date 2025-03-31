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

		string token = sut.GenerateAccessToken([]);

		token.Should().NotBeNull();
	}
}
