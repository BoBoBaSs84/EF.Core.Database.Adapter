using Application.Services.Identity;

using BB84.Home.Domain.Entities.Identity;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;

using Moq;

namespace ApplicationTests.Services.Identity;

public sealed partial class TokenServiceTests
{
	[TestMethod]
	[TestCategory(nameof(TokenService.RemoveRefreshTokenAsync))]
	public async Task RemoveRefreshTokenAsyncShouldReturnResult()
	{
		UserEntity user = CreateUser();
		TokenService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.RemoveAuthenticationTokenAsync(user, It.IsAny<string>(), It.IsAny<string>()))
			.Returns(Task.FromResult(IdentityResult.Success));

		IdentityResult result = await sut.RemoveRefreshTokenAsync(user)
			.ConfigureAwait(false);

		result.Should().NotBeNull();
		_userServiceMock.Verify(x => x.RemoveAuthenticationTokenAsync(user, It.IsAny<string>(), It.IsAny<string>()), Times.Once);
	}
}
