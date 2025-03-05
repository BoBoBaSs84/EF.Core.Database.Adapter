using System.Security.Claims;

using BaseTests.Helpers;

using BB84.Home.Application.Contracts.Requests.Identity;
using BB84.Home.Application.Contracts.Responses.Identity;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Services.Identity;
using BB84.Home.Application.Tests;
using BB84.Home.Domain.Entities.Identity;
using BB84.Home.Domain.Errors;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Identity;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AuthenticationServiceTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(AuthenticationService.Authenticate))]
	public async Task AuthenticateShouldReturnFailedWhenExceptionIsThrown()
	{
		AuthenticationRequest request = CreateAuthenticationRequest();
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByNameAsync(request.UserName))
			.Throws(new InvalidOperationException());

		ErrorOr<AuthenticationResponse> result = await sut.Authenticate(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.AuthenticateUserFailed);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, Exception?>>(), It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.Authenticate))]
	public async Task AuthenticateShouldReturnUnauthorizedWhenUserNotFound()
	{
		AuthenticationRequest request = CreateAuthenticationRequest();
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByNameAsync(request.UserName))
			.Returns(Task.FromResult<UserEntity?>(null));

		ErrorOr<AuthenticationResponse> result = await sut.Authenticate(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserUnauthorized(request.UserName));
			_userServiceMock.Verify(x => x.FindByNameAsync(request.UserName), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, Exception?>>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.Authenticate))]
	public async Task AuthenticateShouldReturnUnauthorizedWhenPasswordCheckIsFalse()
	{
		AuthenticationRequest request = CreateAuthenticationRequest();
		UserEntity user = CreateUser();
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByNameAsync(request.UserName))
			.Returns(Task.FromResult<UserEntity?>(user));
		_userServiceMock.Setup(x => x.CheckPasswordAsync(user, request.Password))
			.Returns(Task.FromResult(false));

		ErrorOr<AuthenticationResponse> result = await sut.Authenticate(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserUnauthorized(request.UserName));
			_userServiceMock.Verify(x => x.FindByNameAsync(request.UserName), Times.Once);
			_userServiceMock.Verify(x => x.CheckPasswordAsync(user, request.Password), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, Exception?>>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.Authenticate))]
	public async Task AuthenticateShouldReturnFailedRefreshTokenCouldNotBeSet()
	{
		string accessToken = RandomHelper.GetString(64);
		string refreshToken = RandomHelper.GetString(64);
		IdentityError error = new() { Code = "UnitTest", Description = "UnitTest" };
		AuthenticationRequest request = CreateAuthenticationRequest();
		UserEntity user = CreateUser();
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByNameAsync(request.UserName))
			.Returns(Task.FromResult<UserEntity?>(user));
		_userServiceMock.Setup(x => x.CheckPasswordAsync(user, request.Password))
			.Returns(Task.FromResult(true));
		_userServiceMock.Setup(x => x.GetRolesAsync(user))
			.Returns(Task.FromResult<IList<string>>(["Tester"]));
		_tokenServiceMock.Setup(x => x.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>()))
			.Returns(accessToken);
		_tokenServiceMock.Setup(x => x.GenerateRefreshTokenAsync(user))
			.Returns(Task.FromResult(refreshToken));
		_tokenServiceMock.Setup(x => x.SetRefreshTokenAsync(user, refreshToken))
			.Returns(Task.FromResult(IdentityResult.Failed(error)));

		ErrorOr<AuthenticationResponse> result = await sut.Authenticate(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.AuthenticateUserFailed);
			_userServiceMock.Verify(x => x.FindByNameAsync(request.UserName), Times.Once);
			_userServiceMock.Verify(x => x.CheckPasswordAsync(user, request.Password), Times.Once);
			_userServiceMock.Verify(x => x.GetRolesAsync(user), Times.Once);
			_tokenServiceMock.Verify(x => x.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>()), Times.Once);
			_tokenServiceMock.Verify(x => x.GenerateRefreshTokenAsync(user), Times.Once);
			_tokenServiceMock.Verify(x => x.SetRefreshTokenAsync(user, refreshToken), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), $"{error.Code} - {error.Description}", It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.Authenticate))]
	public async Task AuthenticateShouldReturnResponseWhenSuccessfulAuthenticated()
	{
		string accessToken = RandomHelper.GetString(64);
		string refreshToken = RandomHelper.GetString(64);
		AuthenticationRequest request = CreateAuthenticationRequest();
		UserEntity user = CreateUser();
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByNameAsync(request.UserName))
			.Returns(Task.FromResult<UserEntity?>(user));
		_userServiceMock.Setup(x => x.CheckPasswordAsync(user, request.Password))
			.Returns(Task.FromResult(true));
		_userServiceMock.Setup(x => x.GetRolesAsync(user))
			.Returns(Task.FromResult<IList<string>>(["Tester"]));
		_tokenServiceMock.Setup(x => x.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>()))
			.Returns(accessToken);
		_tokenServiceMock.Setup(x => x.GenerateRefreshTokenAsync(user))
			.Returns(Task.FromResult(refreshToken));
		_tokenServiceMock.Setup(x => x.SetRefreshTokenAsync(user, refreshToken))
			.Returns(Task.FromResult(IdentityResult.Success));

		ErrorOr<AuthenticationResponse> result = await sut.Authenticate(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.AccessToken.Should().Be(accessToken);
			result.Value.RefreshToken.Should().Be(refreshToken);
			_userServiceMock.Verify(x => x.FindByNameAsync(request.UserName), Times.Once);
			_userServiceMock.Verify(x => x.CheckPasswordAsync(user, request.Password), Times.Once);
			_userServiceMock.Verify(x => x.GetRolesAsync(user), Times.Once);
			_tokenServiceMock.Verify(x => x.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>()), Times.Once);
			_tokenServiceMock.Verify(x => x.GenerateRefreshTokenAsync(user), Times.Once);
			_tokenServiceMock.Verify(x => x.SetRefreshTokenAsync(user, refreshToken), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, Exception?>>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
