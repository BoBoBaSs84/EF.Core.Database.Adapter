using Application.Services.Identity;

using BaseTests.Helpers;

using BB84.Home.Domain.Entities.Identity;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;

using Moq;

namespace ApplicationTests.Services.Identity;

public sealed partial class TokenServiceTests
{
	[TestMethod]
	[TestCategory(nameof(TokenService.SetRefreshTokenAsync))]
	public async Task SetRefreshTokenAsyncShouldReturnResult()
	{
		UserEntity user = CreateUser();
		string token = RandomHelper.GetString(40);
		TokenService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.SetAuthenticationTokenAsync(user, It.IsAny<string>(), It.IsAny<string>(), token))
			.Returns(Task.FromResult(IdentityResult.Success));

		IdentityResult result = await sut.SetRefreshTokenAsync(user, token)
			.ConfigureAwait(false);

		result.Should().NotBeNull();
		_userServiceMock.Verify(x => x.SetAuthenticationTokenAsync(user, It.IsAny<string>(), It.IsAny<string>(), token), Times.Once);
	}
}
