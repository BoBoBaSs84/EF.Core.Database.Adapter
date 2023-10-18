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
	public async Task RemoveUserFromRoleUserNotFound()
	{
		Guid userId = Guid.NewGuid(),
			roleId = Guid.NewGuid();

		ErrorOr<Deleted> result =
			await _authenticationService.RemoveUserFromRole(userId, roleId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.FirstError.Should().Be(AuthenticationServiceErrors.UserByIdNotFound(userId));
		});
	}

	[TestMethod]
	public async Task RemoveUserFromRoleRoleNotFound()
	{
		Guid userId = s_user.Id,
			roleId = Guid.NewGuid();

		ErrorOr<Deleted> result =
			await _authenticationService.RemoveUserFromRole(userId, roleId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.FirstError.Should().Be(AuthenticationServiceErrors.RoleByIdNotFound(roleId));
		});
	}

	[TestMethod]
	public async Task RemoveUserFromRoleSuccess()
	{
		Guid userId = s_user.Id,
			roleId = s_role.Id;

		_ = await _authenticationService.AddUserToRole(userId, roleId);

		ErrorOr<Deleted> result =
			await _authenticationService.RemoveUserFromRole(userId, roleId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
		});
	}

	[TestMethod]
	public async Task RemoveUserFromRoleIdentityError()
	{
		IRoleService roleService = GetService<IRoleService>();

		RoleModel role =
			await roleService.FindByNameAsync(RoleType.SUPERUSER.ToString());

		Guid userId = s_user.Id,
			roleId = role.Id;

		ErrorOr<Deleted> result =
			await _authenticationService.RemoveUserFromRole(userId, roleId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
			result.FirstError.Should().Be(AuthenticationServiceErrors.IdentityError($"UserNotInRole", $"User is not in role 'Super user'."));
		});
	}
}
