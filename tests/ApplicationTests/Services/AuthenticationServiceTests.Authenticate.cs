using Application.Contracts.Requests.Identity;
using Application.Contracts.Responses.Identity;
using Application.Errors.Services;

using BaseTests.Constants;
using BaseTests.Helpers;

using Domain.Errors;

using FluentAssertions;

namespace ApplicationTests.Services;

public partial class AuthenticationServiceTests
{
	[TestMethod]
	public async Task AuthenticateUserNotFound()
	{
		AuthenticationRequest authRequest = new()
		{
			UserName = "UnitTest",
			Password = "UnitTest"
		};

		ErrorOr<AuthenticationResponse> result =
			await _authenticationService.Authenticate(authRequest);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.FirstError.Should().Be(AuthenticationServiceErrors.UserUnauthorized(authRequest.UserName));
		});
	}

	[TestMethod]
	public async Task AuthenticateWrongPassword()
	{
		string userName = s_user.UserName;

		AuthenticationRequest authRequest = new()
		{
			UserName = userName,
			Password = TestConstants.TestUser.BadPassword
		};

		ErrorOr<AuthenticationResponse> result = await _authenticationService.Authenticate(authRequest);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.FirstError.Should().Be(AuthenticationServiceErrors.UserUnauthorized(authRequest.UserName));
		});
	}

	[TestMethod]
	public async Task AuthenticateSuccess()
	{
		string userName = s_user.UserName;

		AuthenticationRequest authRequest = new()
		{
			UserName = userName,
			Password = TestConstants.TestUser.GoodPassword
		};

		ErrorOr<AuthenticationResponse> result =
			await _authenticationService.Authenticate(authRequest);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
		});
	}
}
