using Application.Contracts.Responses.Identity;
using Application.Errors.Services;

using BaseTests.Helpers;

using Domain.Errors;

using FluentAssertions;

namespace ApplicationTests.Services;

public partial class AuthenticationServiceTests
{
	[TestMethod]
	public async Task GetUserByIdSuccess()
	{
		Guid userId = s_user.Id;

		ErrorOr<UserResponse> result =
			await _authenticationService.GetUserById(userId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
		});
	}

	[TestMethod]
	public async Task GetUserByIdNotFound()
	{
		Guid userId = default;

		ErrorOr<UserResponse> result =
			await _authenticationService.GetUserById(userId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
			result.FirstError.Should().Be(AuthenticationServiceErrors.UserByIdNotFound(userId));
		});
	}
}
