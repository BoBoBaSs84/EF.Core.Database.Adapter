using System.Security.Claims;

using BaseTests.Helpers;

using BB84.Home.Application.Contracts.Requests.Identity;
using BB84.Home.Application.Contracts.Responses.Identity;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Services.Identity;
using BB84.Home.Domain.Entities.Identity;
using BB84.Home.Domain.Errors;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Identity;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AuthenticationServiceTests
{
	[TestMethod]
	[TestCategory(nameof(AuthenticationService.RefreshAccessToken))]
	public async Task RefreshAccessTokenShouldReturnFailedWhenExceptionIsThrown()
	{
		TokenRequest request = CreateTokenRequest();
		AuthenticationService sut = CreateMockedInstance();
		_tokenServiceMock.Setup(x => x.GetPrincipalFromExpiredToken(request.AccessToken))
			.Throws(new InvalidOperationException());

		ErrorOr<AuthenticationResponse> result = await sut.RefreshAccessToken(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.RefreshAccessTokenFailed);
			_tokenServiceMock.Verify(x => x.GetPrincipalFromExpiredToken(request.AccessToken), Times.Once());
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, Exception?>>(), It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.RefreshAccessToken))]
	public async Task RefreshAccessTokenShouldReturnNotFoundWhenUserNotFound()
	{
		ClaimsPrincipal principal = CreatePrincipal();
		TokenRequest request = CreateTokenRequest();
		AuthenticationService sut = CreateMockedInstance();
		_tokenServiceMock.Setup(x => x.GetPrincipalFromExpiredToken(request.AccessToken))
			.Returns(principal);
		_userServiceMock.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
			.Returns(Task.FromResult<UserEntity?>(null));

		ErrorOr<AuthenticationResponse> result = await sut.RefreshAccessToken(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.GetUserByNameFailed(principal.Identity!.Name!));
			_tokenServiceMock.Verify(x => x.GetPrincipalFromExpiredToken(request.AccessToken), Times.Once());
			_userServiceMock.Verify(x => x.FindByNameAsync(It.IsAny<string>()), Times.Once());
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, Exception?>>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.RefreshAccessToken))]
	public async Task RefreshAccessTokenShouldReturnFailedWhenTokenVerificationNotSuccessful()
	{
		TokenRequest request = CreateTokenRequest();
		ClaimsPrincipal principal = CreatePrincipal();
		AuthenticationService sut = CreateMockedInstance();
		_tokenServiceMock.Setup(x => x.GetPrincipalFromExpiredToken(request.AccessToken))
			.Returns(principal);
		UserEntity user = CreateUser();
		_userServiceMock.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
			.Returns(Task.FromResult<UserEntity?>(user));
		_tokenServiceMock.Setup(x => x.VerifyRefreshTokenAsync(user, request.RefreshToken))
			.Returns(Task.FromResult(false));

		ErrorOr<AuthenticationResponse> result = await sut.RefreshAccessToken(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.RefreshAccessTokenVerificationFailed);
			_tokenServiceMock.Verify(x => x.GetPrincipalFromExpiredToken(request.AccessToken), Times.Once());
			_userServiceMock.Verify(x => x.FindByNameAsync(It.IsAny<string>()), Times.Once());
			_tokenServiceMock.Verify(x => x.VerifyRefreshTokenAsync(user, request.RefreshToken), Times.Once());
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, Exception?>>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.RefreshAccessToken))]
	public async Task RefreshAccessTokenShouldReturnAuthenticationResponseWhenSuccessful()
	{
		TokenRequest request = CreateTokenRequest();
		ClaimsPrincipal principal = CreatePrincipal();
		AuthenticationService sut = CreateMockedInstance();
		_tokenServiceMock.Setup(x => x.GetPrincipalFromExpiredToken(request.AccessToken))
			.Returns(principal);
		UserEntity user = CreateUser();
		_userServiceMock.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
			.Returns(Task.FromResult<UserEntity?>(user));
		_tokenServiceMock.Setup(x => x.VerifyRefreshTokenAsync(user, request.RefreshToken))
			.Returns(Task.FromResult(true));
		string newAccessToken = RandomHelper.GetString(64);
		_tokenServiceMock.Setup(x => x.GenerateAccessToken(principal.Claims))
			.Returns(newAccessToken);
		string newRefreshToken = RandomHelper.GetString(64);
		_tokenServiceMock.Setup(x => x.GenerateRefreshTokenAsync(user))
			.Returns(Task.FromResult(newRefreshToken));

		ErrorOr<AuthenticationResponse> result = await sut.RefreshAccessToken(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.AccessToken.Should().Be(newAccessToken);
			result.Value.RefreshToken.Should().Be(newRefreshToken);
			_tokenServiceMock.Verify(x => x.GetPrincipalFromExpiredToken(request.AccessToken), Times.Once());
			_userServiceMock.Verify(x => x.FindByNameAsync(It.IsAny<string>()), Times.Once());
			_tokenServiceMock.Verify(x => x.VerifyRefreshTokenAsync(user, request.RefreshToken), Times.Once());
			_tokenServiceMock.Verify(x => x.GenerateAccessToken(principal.Claims), Times.Once());
			_tokenServiceMock.Verify(x => x.GenerateRefreshTokenAsync(user), Times.Once());
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, Exception?>>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
