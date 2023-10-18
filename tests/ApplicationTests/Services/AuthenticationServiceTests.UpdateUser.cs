using Application.Contracts.Requests.Identity;
using Application.Errors.Services;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Results;

using FluentAssertions;

namespace ApplicationTests.Services;

public partial class AuthenticationServiceTests
{
	[TestMethod]
	public async Task UpdateUserSuccessTest()
	{
		Guid userId = s_user.Id;

		UserUpdateRequest updateRequest =
			new UserUpdateRequest().GetUserUpdateRequest();

		ErrorOr<Updated> result =
			await _authenticationService.UpdateUser(userId, updateRequest);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
		});
	}

	[TestMethod]
	public async Task UpdateUserAsyncNotFoundTest()
	{
		Guid userId = Guid.NewGuid();

		UserUpdateRequest updateRequest =
			new UserUpdateRequest().GetUserUpdateRequest();

		ErrorOr<Updated> result =
			await _authenticationService.UpdateUser(userId, updateRequest);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
			result.FirstError.Should().Be(AuthenticationServiceErrors.UserByIdNotFound(userId));
		});
	}

	[TestMethod]
	public async Task UpdateUserIdentityErrorTest()
	{
		Guid userId = s_user.Id;

		UserUpdateRequest updateRequest = new();

		ErrorOr<Updated> result = await _authenticationService.UpdateUser(userId, updateRequest);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
		});
	}
}
