using BB84.Home.Application.Services.Identity;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Identity;

using FluentAssertions;

using Moq;

namespace ApplicationTests.Services.Identity;

public sealed partial class TokenServiceTests
{
	[TestMethod]
	[TestCategory(nameof(TokenService.GenerateRefreshTokenAsync))]
	public async Task GenerateRefreshTokenAsyncShouldReturnResult()
	{
		UserEntity user = CreateUser();
		string token = RandomHelper.GetString(40);
		TokenService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.GenerateUserTokenAsync(user, It.IsAny<string>(), It.IsAny<string>()))
			.Returns(Task.FromResult(token));

		string result = await sut.GenerateRefreshTokenAsync(user)
			.ConfigureAwait(false);

		result.Should().Be(token);
		_userServiceMock.Verify(x => x.GenerateUserTokenAsync(user, It.IsAny<string>(), It.IsAny<string>()), Times.Once);
	}
}
