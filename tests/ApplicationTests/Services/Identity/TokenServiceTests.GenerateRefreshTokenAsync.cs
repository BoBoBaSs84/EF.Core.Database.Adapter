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
	[TestCategory(nameof(TokenService.GenerateRefreshTokenAsync))]
	public async Task GenerateRefreshTokenAsyncShouldReturnResult()
	{
		UserModel user = CreateUser();
		string token = RandomHelper.GetString(40);
		IOptions<BearerSettings> options = GetService<IOptions<BearerSettings>>();
		TokenService sut = CreateMockedInstance(options.Value);
		_userServiceMock.Setup(x => x.GenerateUserTokenAsync(user, It.IsAny<string>(), It.IsAny<string>()))
			.Returns(Task.FromResult(token));

		string result = await sut.GenerateRefreshTokenAsync(user)
			.ConfigureAwait(false);

		result.Should().Be(token);
		_userServiceMock.Verify(x => x.GenerateUserTokenAsync(user, It.IsAny<string>(), It.IsAny<string>()), Times.Once);
	}
}
