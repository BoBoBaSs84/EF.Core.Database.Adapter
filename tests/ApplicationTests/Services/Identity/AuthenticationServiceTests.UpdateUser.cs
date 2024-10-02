﻿using Application.Contracts.Requests.Identity;
using Application.Errors.Services;
using Application.Services.Identity;

using ApplicationTests.Helpers;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Models.Identity;
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
	[TestCategory(nameof(AuthenticationService.UpdateUser))]
	public async Task UpdateUserShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		UserUpdateRequest request = RequestHelper.GetUserUpdateRequest();
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByIdAsync($"{userId}"))
			.Throws(new InvalidOperationException());

		ErrorOr<Updated> result = await sut.UpdateUser(userId, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UpdateUserFailed);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.UpdateUser))]
	public async Task UpdateUserShouldReturnNotFoundWhenUserNotFound()
	{
		Guid userId = Guid.NewGuid();
		UserUpdateRequest request = RequestHelper.GetUserUpdateRequest();
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByIdAsync($"{userId}"))
			.Returns(Task.FromResult<UserModel?>(null));

		ErrorOr<Updated> result = await sut.UpdateUser(userId, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserByIdNotFound(userId));
			_userServiceMock.Verify(x => x.FindByIdAsync($"{userId}"), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.UpdateUser))]
	public async Task UpdateUserShouldReturnFailedWhenUserWasNotCreated()
	{
		Guid userId = Guid.NewGuid();
		UserUpdateRequest request = RequestHelper.GetUserUpdateRequest();
		UserModel user = new();
		IdentityError error = new() { Code = "UnitTest", Description = "UnitTest" };
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByIdAsync($"{userId}"))
			.Returns(Task.FromResult<UserModel?>(user));
		_userServiceMock.Setup(x => x.UpdateAsync(It.IsAny<UserModel>()))
			.Returns(Task.FromResult(IdentityResult.Failed(error)));

		ErrorOr<Updated> result = await sut.UpdateUser(userId, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UpdateUserFailed);
			_userServiceMock.Verify(x => x.FindByIdAsync($"{userId}"), Times.Once);
			_userServiceMock.Verify(x => x.UpdateAsync(It.IsAny<UserModel>()), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), $"{error.Code} - {error.Description}", It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.UpdateUser))]
	public async Task UpdateUserShouldReturnUpdatedWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		UserUpdateRequest request = RequestHelper.GetUserUpdateRequest();
		UserModel user = new() { Id = userId };
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByIdAsync($"{userId}"))
			.Returns(Task.FromResult<UserModel?>(user));
		_userServiceMock.Setup(x => x.UpdateAsync(It.IsAny<UserModel>()))
			.Returns(Task.FromResult(IdentityResult.Success));

		ErrorOr<Updated> result = await sut.UpdateUser(userId, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			user.Id.Should().Be(user.Id);
			user.FirstName.Should().Be(request.FirstName);
			user.MiddleName.Should().Be(request.MiddleName);
			user.LastName.Should().Be(request.LastName);
			user.DateOfBirth.Should().Be(request.DateOfBirth);
			user.Email.Should().Be(request.Email);
			user.PhoneNumber.Should().Be(request.PhoneNumber);
			user.Picture.Should().BeEmpty();
			user.Preferences.Should().Be(request.Preferences);
			_userServiceMock.Verify(x => x.FindByIdAsync($"{userId}"), Times.Once);
			_userServiceMock.Verify(x => x.UpdateAsync(It.IsAny<UserModel>()), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
