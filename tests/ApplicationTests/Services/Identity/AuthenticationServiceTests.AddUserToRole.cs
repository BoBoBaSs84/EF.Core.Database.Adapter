using Application.Errors.Services;
using Application.Services.Identity;

using BaseTests.Helpers;

using BB84.Home.Domain.Entities.Identity;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Identity;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AuthenticationServiceTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(AuthenticationService.AddUserToRole))]
	public async Task AddUserToRoleShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid(), roleId = Guid.NewGuid();
		string[] parameters = [$"{userId}", $"{roleId}"];
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByIdAsync($"{userId}"))
			.Throws(new InvalidOperationException());

		ErrorOr<Created> result = await sut.AddUserToRole(userId, roleId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.AddUserToRoleFailed);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.AddUserToRole))]
	public async Task AddUserToRoleShouldReturnUserNotFoundWhenUserNotFound()
	{
		Guid userId = Guid.NewGuid(), roleId = Guid.NewGuid();
		string[] parameters = [$"{userId}", $"{roleId}"];
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByIdAsync($"{userId}"))
			.Returns(Task.FromResult<UserEntity?>(null));

		ErrorOr<Created> result = await sut.AddUserToRole(userId, roleId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserByIdNotFound(userId));
			_userServiceMock.Verify(x => x.FindByIdAsync($"{userId}"), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.AddUserToRole))]
	public async Task AddUserToRoleShouldReturnRoleNotFoundWhenRoleNotFound()
	{
		Guid userId = Guid.NewGuid(), roleId = Guid.NewGuid();
		string[] parameters = [$"{userId}", $"{roleId}"];
		UserEntity model = CreateUser(userId);
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByIdAsync($"{userId}"))
			.Returns(Task.FromResult<UserEntity?>(model));
		_roleServiceMock.Setup(x => x.FindByIdAsync($"{roleId}"))
			.Returns(Task.FromResult<RoleEntity?>(null));

		ErrorOr<Created> result = await sut.AddUserToRole(userId, roleId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.RoleByIdNotFound(roleId));
			_userServiceMock.Verify(x => x.FindByIdAsync($"{userId}"), Times.Once);
			_roleServiceMock.Verify(x => x.FindByIdAsync($"{roleId}"), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.AddUserToRole))]
	public async Task AddUserToRoleShouldReturnFailedWhenNotSuccessful()
	{
		Guid userId = Guid.NewGuid(), roleId = Guid.NewGuid();
		string[] parameters = [$"{userId}", $"{roleId}"];
		UserEntity user = CreateUser(userId);
		RoleEntity role = new();
		IdentityError error = new() { Code = "UnitTest", Description = "UnitTest" };
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByIdAsync($"{userId}"))
			.Returns(Task.FromResult<UserEntity?>(user));
		_roleServiceMock.Setup(x => x.FindByIdAsync($"{roleId}"))
			.Returns(Task.FromResult<RoleEntity?>(role));
		_userServiceMock.Setup(x => x.AddToRoleAsync(user, role.Name!))
			.Returns(Task.FromResult(IdentityResult.Failed(error)));

		ErrorOr<Created> result = await sut.AddUserToRole(userId, roleId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.AddUserToRoleFailed);
			_userServiceMock.Verify(x => x.FindByIdAsync($"{userId}"), Times.Once);
			_roleServiceMock.Verify(x => x.FindByIdAsync($"{roleId}"), Times.Once);
			_userServiceMock.Verify(x => x.AddToRoleAsync(user, role.Name!), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), $"{error.Code} - {error.Description}", It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.AddUserToRole))]
	public async Task AddUserToRoleShouldReturnCreatedWhenSuccessful()
	{
		Guid userId = Guid.NewGuid(), roleId = Guid.NewGuid();
		string[] parameters = [$"{userId}", $"{roleId}"];
		UserEntity user = CreateUser(userId);
		RoleEntity role = new();
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByIdAsync($"{userId}"))
			.Returns(Task.FromResult<UserEntity?>(user));
		_roleServiceMock.Setup(x => x.FindByIdAsync($"{roleId}"))
			.Returns(Task.FromResult<RoleEntity?>(role));
		_userServiceMock.Setup(x => x.AddToRoleAsync(user, role.Name!))
			.Returns(Task.FromResult(IdentityResult.Success));

		ErrorOr<Created> result = await sut.AddUserToRole(userId, roleId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			_userServiceMock.Verify(x => x.FindByIdAsync($"{userId}"), Times.Once);
			_roleServiceMock.Verify(x => x.FindByIdAsync($"{roleId}"), Times.Once);
			_userServiceMock.Verify(x => x.AddToRoleAsync(user, role.Name!), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
