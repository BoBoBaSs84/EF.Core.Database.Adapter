using Application.Contracts.Responses.Identity;

using BaseTests.Helpers;

using Domain.Errors;

using FluentAssertions;

namespace ApplicationTests.Services;

public partial class AuthenticationServiceTests
{
	[TestMethod]
	public async Task GetAllSuccessTest()
	{
		ErrorOr<IEnumerable<UserResponse>> result =
			await _authenticationService.GetAll();

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNullOrEmpty();
		});
	}
}
