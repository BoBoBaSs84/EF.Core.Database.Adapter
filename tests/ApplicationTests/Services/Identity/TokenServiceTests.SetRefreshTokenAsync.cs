using Application.Options;
using Application.Services.Identity;

using BaseTests.Helpers;

using Domain.Models.Identity;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using Moq;

namespace ApplicationTests.Services.Identity;

public sealed partial class TokenServiceTests
{
	[TestMethod]
	[TestCategory(nameof(TokenService.SetRefreshTokenAsync))]
	public async Task SetRefreshTokenAsyncShouldReturnResult()
	{
		UserModel user = CreateUser();
		string token = RandomHelper.GetString(40);
		IOptions<BearerSettings> options = GetService<IOptions<BearerSettings>>();
		TokenService sut = CreateMockedInstance(options.Value);
		_userServiceMock.Setup(x => x.SetAuthenticationTokenAsync(user, It.IsAny<string>(), It.IsAny<string>(), token))
			.Returns(Task.FromResult(IdentityResult.Success));

		IdentityResult result = await sut.SetRefreshTokenAsync(user, token)
			.ConfigureAwait(false);

		result.Should().NotBeNull();
		_userServiceMock.Verify(x => x.SetAuthenticationTokenAsync(user, It.IsAny<string>(), It.IsAny<string>(), token), Times.Once);
	}
}
