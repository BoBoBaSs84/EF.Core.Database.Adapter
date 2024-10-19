using System.Security.Claims;

using Application.Options;
using Application.Services.Identity;

using BaseTests.Helpers;

using FluentAssertions;

using Microsoft.Extensions.Options;

namespace ApplicationTests.Services.Identity;

public sealed partial class TokenServiceTests
{
	[TestMethod]
	[TestCategory(nameof(TokenService.GetPrincipalFromExpiredToken))]
	public void GetPrincipalFromExpiredTokenShouldReturnPrincipalWhenTokenValid()
	{
		IOptions<BearerSettings> options = GetService<IOptions<BearerSettings>>();
		TokenService sut = CreateMockedInstance(options.Value);
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
