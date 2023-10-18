using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Services;
using BaseTests.Helpers;

using Domain.Enumerators;
using Domain.Errors;
using Domain.Models.Identity;
using Domain.Results;

using FluentAssertions;

namespace ApplicationTests.Services;

public partial class AuthenticationServiceTests
{
	[TestMethod]
	public async Task AddUserToRoleUserNotFound()
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
	public async Task AddUserToRoleRoleNotFound()
	{
		Guid userId = s_user.Id,
			roleId = Guid.NewGuid();

		ErrorOr<Created> result =
			await _authenticationService.AddUserToRole(userId, roleId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.RoleByIdNotFound(roleId));
		});
	}

	[TestMethod]
	public async Task AddUserToRoleSuccess()
	{
		Guid userId = s_user.Id,
			roleId = s_role.Id;

		ErrorOr<Created> result =
			await _authenticationService.AddUserToRole(userId, roleId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
		});
	}

	[TestMethod]
	public async Task AddUserToRoleIdentityError()
	{
		IRoleService roleService = GetService<IRoleService>();

		RoleModel role =
			await roleService.FindByNameAsync(RoleType.USER.ToString());

		Guid userId = s_user.Id,
			roleId = role.Id;

		ErrorOr<Created> result =
			await _authenticationService.AddUserToRole(userId, roleId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
			result.FirstError.Should().Be(AuthenticationServiceErrors.IdentityError($"UserAlreadyInRole", $"User already in role 'User'."));
		});
	}
}
