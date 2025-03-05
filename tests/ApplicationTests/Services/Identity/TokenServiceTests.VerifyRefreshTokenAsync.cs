using Application.Services.Identity;

using BaseTests.Helpers;

using BB84.Home.Domain.Entities.Identity;

using FluentAssertions;

using Moq;

namespace ApplicationTests.Services.Identity;

public sealed partial class TokenServiceTests
{
	[TestMethod]
	[TestCategory(nameof(TokenService.VerifyRefreshTokenAsync))]
	public async Task VerifyRefreshTokenAsyncShouldReturnResult()
	{
		UserEntity user = CreateUser();
		string token = RandomHelper.GetString(40);
		TokenService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.VerifyUserTokenAsync(user, It.IsAny<string>(), It.IsAny<string>(), token))
			.Returns(Task.FromResult(true));

		bool result = await sut.VerifyRefreshTokenAsync(user, token)
			.ConfigureAwait(false);

		result.Should().BeTrue();
		_userServiceMock.Verify(x => x.VerifyUserTokenAsync(user, It.IsAny<string>(), It.IsAny<string>(), token), Times.Once);
	}
}
