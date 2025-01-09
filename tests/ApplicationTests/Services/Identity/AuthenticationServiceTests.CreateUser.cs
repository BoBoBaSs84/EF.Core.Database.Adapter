using Application.Contracts.Requests.Identity;
using Application.Errors.Services;
using Application.Services.Identity;

using ApplicationTests.Helpers;

using BaseTests.Helpers;

using Domain.Entities.Identity;
using Domain.Enumerators;
using Domain.Errors;
using Domain.Extensions;
using Domain.Results;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Identity;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AuthenticationServiceTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(AuthenticationService.CreateUser))]
	public async Task CreateUserShouldReturnFailedWhenExceptionIsThrown()
	{
		UserCreateRequest request = RequestHelper.GetUserCreateRequest();
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.CreateAsync(It.IsAny<UserEntity>(), request.Password))
			.Throws(new InvalidOperationException());

		ErrorOr<Created> result = await sut.CreateUser(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.CreateUserFailed);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.CreateUser))]
	public async Task CreateUserShouldReturnFailedWhenUserWasNotCreated()
	{
		UserCreateRequest request = RequestHelper.GetUserCreateRequest();
		IdentityError error = new() { Code = "UnitTest", Description = "UnitTest" };
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.CreateAsync(It.IsAny<UserEntity>(), request.Password))
			.Returns(Task.FromResult(IdentityResult.Failed(error)));

		ErrorOr<Created> result = await sut.CreateUser(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.CreateUserFailed);
			_userServiceMock.Verify(x => x.CreateAsync(It.IsAny<UserEntity>(), request.Password), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), $"{error.Code} - {error.Description}", It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.CreateUser))]
	public async Task CreateUserShouldReturnFailedWhenRoleWasNotCreated()
	{
		UserCreateRequest request = RequestHelper.GetUserCreateRequest();
		IdentityError error = new() { Code = "UnitTest", Description = "UnitTest" };
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.CreateAsync(It.IsAny<UserEntity>(), request.Password))
			.Returns(Task.FromResult(IdentityResult.Success));
		_userServiceMock.Setup(x => x.AddToRoleAsync(It.IsAny<UserEntity>(), RoleType.USER.GetName()))
			.Returns(Task.FromResult(IdentityResult.Failed(error)));

		ErrorOr<Created> result = await sut.CreateUser(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.AddUserToRoleFailed);
			_userServiceMock.Verify(x => x.CreateAsync(It.IsAny<UserEntity>(), request.Password), Times.Once);
			_userServiceMock.Verify(x => x.AddToRoleAsync(It.IsAny<UserEntity>(), RoleType.USER.GetName()), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), $"{error.Code} - {error.Description}", It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.CreateUser))]
	public async Task CreateUserShouldReturnCreatedWhenSuccessful()
	{
		UserCreateRequest request = RequestHelper.GetUserCreateRequest();
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.CreateAsync(It.IsAny<UserEntity>(), request.Password))
			.Returns(Task.FromResult(IdentityResult.Success));
		_userServiceMock.Setup(x => x.AddToRoleAsync(It.IsAny<UserEntity>(), RoleType.USER.GetName()))
			.Returns(Task.FromResult(IdentityResult.Success));

		ErrorOr<Created> result = await sut.CreateUser(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			_userServiceMock.Verify(x => x.CreateAsync(It.IsAny<UserEntity>(), request.Password), Times.Once);
			_userServiceMock.Verify(x => x.AddToRoleAsync(It.IsAny<UserEntity>(), RoleType.USER.GetName()), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
