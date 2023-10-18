using Application.Contracts.Requests.Identity;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Results;

using FluentAssertions;

using TU = BaseTests.Constants.TestConstants.TestUser;

namespace ApplicationTests.Services;

public partial class AuthenticationServiceTests
{
	[TestMethod]
	public async Task CreateUserIdentityErrorTest()
	{
		UserCreateRequest createRequest =
			new UserCreateRequest().GetUserCreateRequest(TU.BadPassword);

		ErrorOr<Created> result =
			await _authenticationService.CreateUser(createRequest);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
		});
	}

	[TestMethod]
	public async Task CreateUserSuccessTest()
	{
		UserCreateRequest createRequest =
			new UserCreateRequest().GetUserCreateRequest();

		ErrorOr<Created> result =
			await _authenticationService.CreateUser(createRequest);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
		});
	}
}
