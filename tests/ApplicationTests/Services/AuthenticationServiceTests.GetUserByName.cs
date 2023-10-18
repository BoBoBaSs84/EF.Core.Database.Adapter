using Application.Contracts.Responses.Identity;
using Application.Errors.Services;

using BaseTests.Helpers;

using Domain.Errors;

using FluentAssertions;

namespace ApplicationTests.Services;

public partial class AuthenticationServiceTests
{
	[TestMethod]
	public async Task GetUserByNameSuccess()
	{
		string userName = s_user.UserName;

		ErrorOr<UserResponse> result =
			await _authenticationService.GetUserByName(userName);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
		});
	}

	[TestMethod]
	public async Task GetUserByNameNotFound()
	{
		string userName = "UnitTest";

		ErrorOr<UserResponse> result =
			await _authenticationService.GetUserByName(userName);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserByNameNotFound(userName));
		});
	}
}
