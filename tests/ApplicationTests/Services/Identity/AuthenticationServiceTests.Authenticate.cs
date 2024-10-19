using Application.Contracts.Requests.Identity;
using Application.Contracts.Responses.Identity;
using Application.Errors.Services;
using Application.Options;
using Application.Services.Identity;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Models.Identity;

using FluentAssertions;

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
		AuthenticationRequest request = GetRequest();
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
		AuthenticationRequest request = GetRequest();
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByNameAsync(request.UserName))
			.Returns(Task.FromResult<UserModel?>(null));

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
		AuthenticationRequest request = GetRequest();
		UserModel user = new();
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByNameAsync(request.UserName))
			.Returns(Task.FromResult<UserModel?>(user));
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
	public async Task AuthenticateShouldReturnResponseWhenSuccessfulAuthenticated()
	{
		BearerSettings settings = new()
		{
			Issuer = "UnitTest",
			Audience = "http://UnitTest.org",
			ExpiryInMinutes = 5,
			SecurityKey = RandomHelper.GetString(64)
		};
		AuthenticationRequest request = GetRequest();
		UserModel user = CreateUser();
		AuthenticationService sut = CreateMockedInstance(settings);
		_userServiceMock.Setup(x => x.FindByNameAsync(request.UserName))
			.Returns(Task.FromResult<UserModel?>(user));
		_userServiceMock.Setup(x => x.CheckPasswordAsync(user, request.Password))
			.Returns(Task.FromResult(true));
		_userServiceMock.Setup(x => x.GetRolesAsync(user))
			.Returns(Task.FromResult<IList<string>>(["Tester"]));

		ErrorOr<AuthenticationResponse> result = await sut.Authenticate(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.AccessToken.Should().NotBeNull();
			result.Value.RefreshToken.Should().NotBeNull();
			_userServiceMock.Verify(x => x.FindByNameAsync(request.UserName), Times.Once);
			_userServiceMock.Verify(x => x.CheckPasswordAsync(user, request.Password), Times.Once);
			_userServiceMock.Verify(x => x.GetRolesAsync(user), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, Exception?>>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	private static AuthenticationRequest GetRequest()
		=> new() { UserName = "UserName", Password = "Password" };
}
