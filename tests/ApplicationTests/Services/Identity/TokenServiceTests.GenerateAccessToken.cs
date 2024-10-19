using Application.Options;
using Application.Services.Identity;

using FluentAssertions;

using Microsoft.Extensions.Options;

namespace ApplicationTests.Services.Identity;

public sealed partial class TokenServiceTests
{
	[TestMethod]
	[TestCategory(nameof(TokenService.GenerateAccessToken))]
	public void GenerateAccessTokenShouldGenerateAccessToken()
	{
		IOptions<BearerSettings> options = GetService<IOptions<BearerSettings>>();
		TokenService sut = CreateMockedInstance(options.Value);

		string token = sut.GenerateAccessToken([]);

		token.Should().NotBeNull();
	}
}
