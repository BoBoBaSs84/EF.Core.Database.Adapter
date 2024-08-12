using Application.Contracts.Requests.Identity;
using Application.Errors.Services;
using Application.Services.Identity;

using BaseTests.Helpers;

using Domain.Enumerators;
using Domain.Errors;
using Domain.Extensions;
using Domain.Models.Identity;
using Domain.Results;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Moq;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApplicationTests.Services.Identity;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AuthenticationServiceTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(AuthenticationService.CreateUser))]
	public async Task CreateUserShouldReturnFailedWhenExceptionIsThrown()
	{
		UserCreateRequest request = new();
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.CreateAsync(It.IsAny<UserModel>(), request.Password))
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
		UserCreateRequest request = new();
		IdentityError error = new() { Code = "UnitTest", Description = "UnitTest" };
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.CreateAsync(It.IsAny<UserModel>(), request.Password))
			.Returns(Task.FromResult(IdentityResult.Failed(error)));

		ErrorOr<Created> result = await sut.CreateUser(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.CreateUserFailed);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), $"{error.Code} - {error.Description}", It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.CreateUser))]
	public async Task CreateUserShouldReturnFailedWhenRoleWasNotCreated()
	{
		UserCreateRequest request = new();
		IdentityError error = new() { Code = "UnitTest", Description = "UnitTest" };
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.CreateAsync(It.IsAny<UserModel>(), request.Password))
			.Returns(Task.FromResult(IdentityResult.Success));
		_userServiceMock.Setup(x => x.AddToRoleAsync(It.IsAny<UserModel>(), RoleType.USER.GetName()))
			.Returns(Task.FromResult(IdentityResult.Failed(error)));

		ErrorOr<Created> result = await sut.CreateUser(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.AddUserToRoleFailed);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), $"{error.Code} - {error.Description}", It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.CreateUser))]
	public async Task CreateUserShouldReturnCreatedWhenSuccessful()
	{
		UserCreateRequest request = new();
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.CreateAsync(It.IsAny<UserModel>(), request.Password))
			.Returns(Task.FromResult(IdentityResult.Success));
		_userServiceMock.Setup(x => x.AddToRoleAsync(It.IsAny<UserModel>(), RoleType.USER.GetName()))
			.Returns(Task.FromResult(IdentityResult.Success));

		ErrorOr<Created> result = await sut.CreateUser(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
