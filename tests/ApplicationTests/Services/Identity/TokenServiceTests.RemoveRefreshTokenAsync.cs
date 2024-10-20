using Application.Options;
using Application.Services.Identity;

using Domain.Models.Identity;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using Moq;

namespace ApplicationTests.Services.Identity;

public sealed partial class TokenServiceTests
{
	[TestMethod]
	[TestCategory(nameof(TokenService.RemoveRefreshTokenAsync))]
	public async Task RemoveRefreshTokenAsyncShouldReturnResult()
	{
		UserModel user = CreateUser();
		IOptions<BearerSettings> options = GetService<IOptions<BearerSettings>>();
		TokenService sut = CreateMockedInstance(options.Value);
		_userServiceMock.Setup(x => x.RemoveAuthenticationTokenAsync(user, It.IsAny<string>(), It.IsAny<string>()))
			.Returns(Task.FromResult(IdentityResult.Success));

		IdentityResult result = await sut.RemoveRefreshTokenAsync(user)
			.ConfigureAwait(false);

		result.Should().NotBeNull();
		_userServiceMock.Verify(x => x.RemoveAuthenticationTokenAsync(user, It.IsAny<string>(), It.IsAny<string>()), Times.Once);
	}
}
