using Application.Contracts.Requests.Identity;
using Application.Contracts.Responses.Identity;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Services;

using ApplicationTests.Helpers;

using Domain.Errors;
using Domain.Results;

using FluentAssertions;

using AH = BaseTests.Helpers.AssertionHelper;
using TC = BaseTests.Constants.TestConstants;
using TU = BaseTests.Constants.TestConstants.TestUser;

namespace ApplicationTests.Services;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, UnitTest.")]
public class AuthenticationServiceTests : ApplicationBaseTests
{
	private IAuthenticationService _authenticationService = default!;

	[TestMethod, Owner(TC.Bobo)]
	public async Task CreateUserAsyncIdentityErrorTest()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();

		UserCreateRequest createRequest =
			new UserCreateRequest().GetUserCreateRequest(TU.PassBad);

		ErrorOr<Created> result =
			await _authenticationService.CreateUser(createRequest);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task CreateUserAsyncSuccessTest()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();

		UserCreateRequest createRequest =
			new UserCreateRequest().GetUserCreateRequest();

		ErrorOr<Created> result =
			await _authenticationService.CreateUser(createRequest);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task GetAllAsyncSuccessTest()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();

		UserCreateRequest createRequest =
			new UserCreateRequest().GetUserCreateRequest();

		_ = await _authenticationService.CreateUser(createRequest);

		ErrorOr<IEnumerable<UserResponse>> result =
			await _authenticationService.GetAll();

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNullOrEmpty();
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task GetUserByNameAsyncSuccessTest()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();

		UserCreateRequest createRequest =
			new UserCreateRequest().GetUserCreateRequest();

		_ = await _authenticationService.CreateUser(createRequest);

		ErrorOr<UserResponse> result =
			await _authenticationService.GetUserByName(createRequest.UserName);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task GetUserByIdAsyncSuccessTest()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();

		UserCreateRequest createRequest =
			new UserCreateRequest().GetUserCreateRequest();

		_ = await _authenticationService.CreateUser(createRequest);

		ErrorOr<IEnumerable<UserResponse>> user =
			await _authenticationService.GetAll();

		ErrorOr<UserResponse> result =
			await _authenticationService.GetUserById(user.Value.First().Id);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task GetUserByNameAsyncNotFoundTest()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();
		string userName = "UnitTest";

		ErrorOr<UserResponse> result =
			await _authenticationService.GetUserByName(userName);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserByNameNotFound(userName));
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task GetUserByIdAsyncNotFoundTest()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();
		Guid userId = Guid.NewGuid();

		ErrorOr<UserResponse> result =
			await _authenticationService.GetUserById(userId);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserByIdNotFound(userId));
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task UpdateUserAsyncSuccessTest()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();

		UserCreateRequest createRequest =
			new UserCreateRequest().GetUserCreateRequest();

		_ = await _authenticationService.CreateUser(createRequest);

		ErrorOr<UserResponse> user =
			await _authenticationService.GetUserByName(createRequest.UserName);

		UserUpdateRequest updateRequest =
			new UserUpdateRequest().GetUserUpdateRequest();

		ErrorOr<Updated> result =
			await _authenticationService.UpdateUser(user.Value.Id, updateRequest);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task UpdateUserAsyncNotFoundTest()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();
		Guid userId = Guid.NewGuid();

		UserUpdateRequest updateRequest =
			new UserUpdateRequest().GetUserUpdateRequest();

		ErrorOr<Updated> result =
			await _authenticationService.UpdateUser(userId, updateRequest);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserByIdNotFound(userId));
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task UpdateUserAsyncIdentityErrorTest()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();

		UserCreateRequest createRequest =
			new UserCreateRequest().GetUserCreateRequest();

		_ = await _authenticationService.CreateUser(createRequest);

		ErrorOr<UserResponse> user =
			await _authenticationService.GetUserByName(createRequest.UserName);

		UserUpdateRequest updateRequest = new();

		ErrorOr<Updated> result = await _authenticationService.UpdateUser(user.Value.Id, updateRequest);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task AddUserToRoleUserNotFoundAsync()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();
		Guid userId = Guid.NewGuid();

		ErrorOr<Created> result =
			await _authenticationService.AddUserToRole(userId, "test");

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserByIdNotFound(userId));
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task AddUserToRoleRoleNotFoundAsync()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();
		string roleName = "TestRole";

		UserCreateRequest createRequest =
			new UserCreateRequest().GetUserCreateRequest();

		_ = await _authenticationService.CreateUser(createRequest);

		ErrorOr<UserResponse> user =
			await _authenticationService.GetUserByName(createRequest.UserName);

		ErrorOr<Created> result =
			await _authenticationService.AddUserToRole(user.Value.Id, roleName);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.RoleByNameNotFound(roleName));
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task AddUserToRoleIdentityError()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();
		string roleName = "User";

		UserCreateRequest createRequest =
			new UserCreateRequest().GetUserCreateRequest();

		_ = await _authenticationService.CreateUser(createRequest);

		ErrorOr<UserResponse> user =
			await _authenticationService.GetUserByName(createRequest.UserName);

		ErrorOr<Created> result =
			await _authenticationService.AddUserToRole(user.Value.Id, roleName);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task AddUserToRoleSuccess()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();
		string roleName = "SuperUser";

		UserCreateRequest createRequest =
			new UserCreateRequest().GetUserCreateRequest();

		_ = await _authenticationService.CreateUser(createRequest);

		ErrorOr<UserResponse> user =
			await _authenticationService.GetUserByName(createRequest.UserName);

		ErrorOr<Created> result =
			await _authenticationService.AddUserToRole(user.Value.Id, roleName);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task AuthenticateUserNotFound()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();

		UserCreateRequest createRequest =
			new UserCreateRequest().GetUserCreateRequest();

		_ = await _authenticationService.CreateUser(createRequest);

		AuthenticationRequest authRequest = new()
		{
			UserName = "UnitTest",
			Password = createRequest.Password
		};

		ErrorOr<AuthenticationResponse> result =
			await _authenticationService.Authenticate(authRequest);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserUnauthorized(authRequest.UserName));
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task AuthenticateUnauthorized()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();

		UserCreateRequest createRequest = new();
		createRequest.GetUserCreateRequest();

		_ = await _authenticationService.CreateUser(createRequest);

		AuthenticationRequest authRequest = new()
		{
			UserName = createRequest.UserName,
			Password = "UnitTest"
		};
		ErrorOr<AuthenticationResponse> result = await _authenticationService.Authenticate(authRequest);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserUnauthorized(authRequest.UserName));
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task AuthenticateSuccess()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();

		UserCreateRequest createRequest =
			new UserCreateRequest().GetUserCreateRequest();

		_ = await _authenticationService.CreateUser(createRequest);

		AuthenticationRequest authRequest = new()
		{
			UserName = createRequest.UserName,
			Password = createRequest.Password
		};

		ErrorOr<AuthenticationResponse> result =
			await _authenticationService.Authenticate(authRequest);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task RemoveUserToRoleUserNotFoundAsync()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();
		Guid userId = Guid.NewGuid();

		ErrorOr<Deleted> result =
			await _authenticationService.RemoveUserToRole(userId, "test");

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserByIdNotFound(userId));
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task RemoveUserToRoleRoleNotFoundAsync()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();
		string roleName = "TestRole";

		UserCreateRequest createRequest =
			new UserCreateRequest().GetUserCreateRequest();

		_ = await _authenticationService.CreateUser(createRequest);

		ErrorOr<UserResponse> user =
			await _authenticationService.GetUserByName(createRequest.UserName);

		ErrorOr<Deleted> result =
			await _authenticationService.RemoveUserToRole(user.Value.Id, roleName);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.RoleByNameNotFound(roleName));
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task RemoveUserToRoleIdentityError()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();
		string roleName = "SuperUser";

		UserCreateRequest createRequest =
			new UserCreateRequest().GetUserCreateRequest();

		_ = await _authenticationService.CreateUser(createRequest);

		ErrorOr<UserResponse> user =
			await _authenticationService.GetUserByName(createRequest.UserName);

		ErrorOr<Deleted> result =
			await _authenticationService.RemoveUserToRole(user.Value.Id, roleName);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task RemoveUserToRoleSuccess()
	{
		_authenticationService = GetRequiredService<IAuthenticationService>();
		string roleName = "User";

		UserCreateRequest createRequest =
			new UserCreateRequest().GetUserCreateRequest();

		_ = await _authenticationService.CreateUser(createRequest);

		ErrorOr<UserResponse> user =
			await _authenticationService.GetUserByName(createRequest.UserName);

		ErrorOr<Deleted> result =
			await _authenticationService.RemoveUserToRole(user.Value.Id, roleName);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
		});
	}
}