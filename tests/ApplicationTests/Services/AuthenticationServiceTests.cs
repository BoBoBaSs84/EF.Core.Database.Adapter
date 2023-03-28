using Application.Contracts.Requests.Identity;
using Application.Contracts.Responses.Identity;
using Application.Interfaces.Application;
using ApplicationTests.Helpers;
using BaseTests.Attributes;
using Domain.Errors;
using Domain.Results;
using FluentAssertions;
using static BaseTests.Helpers.AssertionHelper;
using TC = BaseTests.Constants.TestConstants;
using TU = BaseTests.Constants.TestConstants.TestUser;

namespace ApplicationTests.Services;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class AuthenticationServiceTests : ApplicationBaseTests
{
	private IAuthenticationService _authenticationService = default!;

	[TestMethod, Owner(TC.Bobo), TestOrder(1)]
	public async Task CreateUserAsyncIdentityErrorTest()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();

		UserCreateRequest createRequest = new();
		createRequest.GetUserCreateRequest(TU.UserName, TU.PassBad);

		ErrorOr<Created> result = await _authenticationService.CreateUser(createRequest);

		AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
		});
	}

	[TestMethod, Owner(TC.Bobo), TestOrder(2)]
	public async Task CreateUserAsyncSuccessTest()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();

		UserCreateRequest createRequest = new();
		createRequest.GetUserCreateRequest(TU.UserName, TU.PassGood);

		ErrorOr<Created> result = await _authenticationService.CreateUser(createRequest);

		AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
		});
	}

	[TestMethod, Owner(TC.Bobo), TestOrder(3)]
	public async Task GetAllAsync()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();

		ErrorOr<IEnumerable<UserResponse>> result = await _authenticationService.GetAll();

		AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNullOrEmpty();
		});
	}

	[TestMethod, Owner(TC.Bobo), TestOrder(4)]
	public async Task GetUserByNameAsync()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();

		ErrorOr<UserResponse> result = await _authenticationService.GetUserByName(TU.UserName);

		AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
		});
	}

	[TestMethod, Owner(TC.Bobo), TestOrder(5)]
	public async Task GetUserByIdAsync()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();

		ErrorOr<UserResponse> result = await _authenticationService.GetUserById(1);

		AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
		});
	}

	[TestMethod, Owner(TC.Bobo), TestOrder(6)]
	public async Task UpdateUserAsync()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();

		UserUpdateRequest updateRequest = new();
		updateRequest.GetUserUpdateRequest();

		ErrorOr<Updated> result = await _authenticationService.UpdateUser(1, updateRequest);

		AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
		});
	}
}