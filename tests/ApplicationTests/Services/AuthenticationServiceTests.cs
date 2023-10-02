using Application.Contracts.Requests.Identity;
using Application.Contracts.Responses.Identity;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Services;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Results;
using Domain.Models.Identity;

using FluentAssertions;

using TU = BaseTests.Constants.TestConstants.TestUser;
using BaseTests.Constants;

namespace ApplicationTests.Services;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, UnitTest.")]
public class AuthenticationServiceTests : ApplicationTestBase
{
	private readonly IAuthenticationService _authenticationService;

	public AuthenticationServiceTests()
		=> _authenticationService = GetService<IAuthenticationService>();

	[TestMethod]
	public async Task CreateUserAsyncIdentityErrorTest()
	{
		UserCreateRequest createRequest =
			new UserCreateRequest().GetUserCreateRequest(TU.PassBad);

		ErrorOr<Created> result =
			await _authenticationService.CreateUser(createRequest);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
		});
	}

	[TestMethod]
	public async Task CreateUserAsyncSuccessTest()
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

	[TestMethod]
	public async Task GetAllAsyncSuccessTest()
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

	[TestMethod]
	public async Task GetUserByNameAsyncSuccessTest()
	{
		string userName =
			Users[RandomHelper.GetInt(0, Users.Count)].UserName;

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
	public async Task GetUserByIdAsyncSuccessTest()
	{
		string userName =
			Users[RandomHelper.GetInt(0, Users.Count)].UserName;

		ErrorOr<UserResponse> result =
			await _authenticationService.GetUserByName(userName);

		result =
			await _authenticationService.GetUserById(result.Value.Id);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
		});
	}

	[TestMethod]
	public async Task GetUserByNameAsyncNotFoundTest()
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

	[TestMethod]
	public async Task GetUserByIdAsyncNotFoundTest()
	{
		Guid userId = Guid.NewGuid();

		ErrorOr<UserResponse> result =
			await _authenticationService.GetUserById(userId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserByIdNotFound(userId));
		});
	}

	[TestMethod]
	public async Task UpdateUserAsyncSuccessTest()
	{
		string userName =
			Users[RandomHelper.GetInt(0, Users.Count)].UserName;

		ErrorOr<UserResponse> user =
			await _authenticationService.GetUserByName(userName);

		UserUpdateRequest updateRequest =
			new UserUpdateRequest().GetUserUpdateRequest();

		ErrorOr<Updated> result =
			await _authenticationService.UpdateUser(user.Value.Id, updateRequest);

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
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserByIdNotFound(userId));
		});
	}

	[TestMethod]
	public async Task UpdateUserAsyncIdentityErrorTest()
	{
		string userName =
			Users[RandomHelper.GetInt(0, Users.Count)].UserName;

		ErrorOr<UserResponse> user =
			await _authenticationService.GetUserByName(userName);

		UserUpdateRequest updateRequest = new();

		ErrorOr<Updated> result = await _authenticationService.UpdateUser(user.Value.Id, updateRequest);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
		});
	}

	[TestMethod]
	public async Task AddUserToRoleUserNotFoundAsync()
	{
		Guid userId = Guid.NewGuid(),
			roleId = Guid.NewGuid();

		ErrorOr<Created> result =
			await _authenticationService.AddUserToRole(userId, roleId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserByIdNotFound(userId));
		});
	}

	[TestMethod]
	public async Task AddUserToRoleRoleNotFoundAsync()
	{
		string userName =
			Users[RandomHelper.GetInt(0, Users.Count)].UserName;

		Guid roleId = Guid.NewGuid();

		ErrorOr<UserResponse> user =
			await _authenticationService.GetUserByName(userName);

		ErrorOr<Created> result =
			await _authenticationService.AddUserToRole(user.Value.Id, roleId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.RoleByIdNotFound(roleId));
		});
	}

	[TestMethod]
	public async Task AddUserToRoleSuccess()
	{
		string userName =
			Users[RandomHelper.GetInt(0, Users.Count)].UserName;

		Guid roleId = Roles[0].Id;

		ErrorOr<UserResponse> user =
			await _authenticationService.GetUserByName(userName);

		ErrorOr<Created> result =
			await _authenticationService.AddUserToRole(user.Value.Id, roleId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
		});
	}

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
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserUnauthorized(authRequest.UserName));
		});
	}

	[TestMethod]
	public async Task AuthenticateUnauthorized()
	{
		string userName =
			Users[RandomHelper.GetInt(0, Users.Count)].UserName;

		AuthenticationRequest authRequest = new()
		{
			UserName = userName,
			Password = "UnitTest"
		};
		ErrorOr<AuthenticationResponse> result = await _authenticationService.Authenticate(authRequest);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserUnauthorized(authRequest.UserName));
		});
	}

	[TestMethod]
	public async Task AuthenticateSuccess()
	{
		string userName =
			Users[RandomHelper.GetInt(0, Users.Count)].UserName;

		AuthenticationRequest authRequest = new()
		{
			UserName = userName,
			Password = TU.PassGood
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

	[TestMethod]
	public async Task RemoveUserFromRoleUserNotFoundAsync()
	{
		Guid userId = Guid.NewGuid(),
			roleId = Guid.NewGuid();

		ErrorOr<Deleted> result =
			await _authenticationService.RemoveUserFromRole(userId, roleId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserByIdNotFound(userId));
		});
	}

	[TestMethod]
	public async Task RemoveUserFromRoleRoleNotFoundAsync()
	{
		string userName =
			Users[RandomHelper.GetInt(0, Users.Count)].UserName;

		Guid roleId = Guid.NewGuid();

		ErrorOr<UserResponse> user =
			await _authenticationService.GetUserByName(userName);

		ErrorOr<Deleted> result =
			await _authenticationService.RemoveUserFromRole(user.Value.Id, roleId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.RoleByIdNotFound(roleId));
		});
	}

	[TestMethod]
	public async Task RemoveUserFromRoleSuccess()
	{
		string userName =
			Users[RandomHelper.GetInt(0, Users.Count)].UserName;

		Guid roleId = Roles[0].Id;

		ErrorOr<UserResponse> user =
			await _authenticationService.GetUserByName(userName);

		_ = await _authenticationService.AddUserToRole(user.Value.Id, roleId);

		ErrorOr<Deleted> result =
			await _authenticationService.RemoveUserFromRole(user.Value.Id, roleId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
		});
	}
}