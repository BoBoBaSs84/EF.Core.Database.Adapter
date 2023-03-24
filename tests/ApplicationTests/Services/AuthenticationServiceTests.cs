using Application.Contracts.Requests.Identity;
using Application.Interfaces.Application;
using Domain.Errors;
using Domain.Results;
using FluentAssertions;
using TC = BaseTests.Constants.TestConstants;
using TU = BaseTests.Constants.TestConstants.TestUser;
using static BaseTests.Helpers.AssertionHelper;

namespace ApplicationTests.Services;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class AuthenticationServiceTests : ApplicationBaseTests
{
	private IAuthenticationService _authenticationService = default!;

	[TestMethod, Owner(TC.Bobo)]
	public async Task CreateUserAsyncIdentityErrorTest()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();

		UserCreateRequest createRequest = new()
		{
			UserName = TU.UserName,
			Email = TU.Email,
			Password = TU.PassBad,
		};

		ErrorOr<Created> result = await _authenticationService.CreateUser(createRequest);

		AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
		});		
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task CreateUserAsyncSuccessTest()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();

		UserCreateRequest createRequest = new()
		{
			FirstName = TU.FirstName,
			LastName = TU.LastName,
			UserName = TU.UserName,
			Email = TU.Email,
			Password = TU.PassGood,
		};

		ErrorOr<Created> result = await _authenticationService.CreateUser(createRequest);

		AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
		});
	}
}