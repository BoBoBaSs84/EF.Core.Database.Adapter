using Application.Errors.Services;
using Application.Services.Identity;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Models.Identity;
using Domain.Results;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Identity;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AuthenticationServiceTests
{
	[TestMethod]
	[TestCategory(nameof(AuthenticationService.RevokeRefreshToken))]
	public async Task RevokeRefreshTokenShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByIdAsync($"{userId}"))
			.Throws(new InvalidOperationException());

		ErrorOr<Deleted> result = await sut.RevokeRefreshToken(userId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.RevokeRefreshTokenFailed);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), userId, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.RevokeRefreshToken))]
	public async Task RevokeRefreshTokenShouldReturnUserNotFoundWhenUserNotFound()
	{
		Guid userId = Guid.NewGuid();
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByIdAsync($"{userId}"))
			.Returns(Task.FromResult<UserModel?>(null));

		ErrorOr<Deleted> result = await sut.RevokeRefreshToken(userId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserByIdNotFound(userId));
			_userServiceMock.Verify(x => x.FindByIdAsync($"{userId}"), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.RevokeRefreshToken))]
	public async Task RevokeRefreshTokenShouldReturnFailedWhenNotSuccessful()
	{
		Guid userId = Guid.NewGuid();
		UserModel user = CreateUser(userId);
		IdentityError error = new() { Code = "Error", Description = "UnitTest" };
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByIdAsync($"{userId}"))
			.Returns(Task.FromResult<UserModel?>(user));
		_tokenServiceMock.Setup(x => x.RemoveRefreshTokenAsync(user))
			.Returns(Task.FromResult(IdentityResult.Failed(error)));

		ErrorOr<Deleted> result = await sut.RevokeRefreshToken(userId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.RevokeRefreshTokenFailed);
			_userServiceMock.Verify(x => x.FindByIdAsync($"{userId}"), Times.Once);
			_tokenServiceMock.Verify(x => x.RemoveRefreshTokenAsync(user), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), $"{error.Code} - {error.Description}", It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.RevokeRefreshToken))]
	public async Task RevokeRefreshTokenShouldReturnDeletedWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		UserModel user = CreateUser(userId);
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByIdAsync($"{userId}"))
			.Returns(Task.FromResult<UserModel?>(user));
		_tokenServiceMock.Setup(x => x.RemoveRefreshTokenAsync(user))
			.Returns(Task.FromResult(IdentityResult.Success));

		ErrorOr<Deleted> result = await sut.RevokeRefreshToken(userId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
			_userServiceMock.Verify(x => x.FindByIdAsync($"{userId}"), Times.Once);
			_tokenServiceMock.Verify(x => x.RemoveRefreshTokenAsync(user), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
