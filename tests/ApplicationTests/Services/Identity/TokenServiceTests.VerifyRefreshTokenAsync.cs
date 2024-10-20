using Application.Options;
using Application.Services.Identity;

using BaseTests.Helpers;

using Domain.Models.Identity;

using FluentAssertions;

using Microsoft.Extensions.Options;

using Moq;

namespace ApplicationTests.Services.Identity;

public sealed partial class TokenServiceTests
{
	[TestMethod]
	[TestCategory(nameof(TokenService.VerifyRefreshTokenAsync))]
	public async Task VerifyRefreshTokenAsyncShouldReturnResult()
	{
		UserModel user = CreateUser();
		string token = RandomHelper.GetString(40);
		IOptions<BearerSettings> options = GetService<IOptions<BearerSettings>>();
		TokenService sut = CreateMockedInstance(options.Value);
		_userServiceMock.Setup(x => x.VerifyUserTokenAsync(user, It.IsAny<string>(), It.IsAny<string>(), token))
			.Returns(Task.FromResult(true));

		bool result = await sut.VerifyRefreshTokenAsync(user, token)
			.ConfigureAwait(false);

		result.Should().BeTrue();
		_userServiceMock.Verify(x => x.VerifyUserTokenAsync(user, It.IsAny<string>(), It.IsAny<string>(), token), Times.Once);
	}
}
