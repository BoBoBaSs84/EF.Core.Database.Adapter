using Application.Contracts.Responses.Identity;
using Application.Errors.Services;
using Application.Services.Identity;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Models.Identity;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Identity;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AuthenticationServiceTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(AuthenticationService.GetUserById))]
	public async Task GetUserByIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByIdAsync($"{userId}"))
			.Throws(new InvalidOperationException());

		ErrorOr<UserResponse> result = await sut.GetUserById(userId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.GetUserByIdFailed(userId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), userId, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.GetUserById))]
	public async Task GetUserByIdShouldReturnNotFoundWhenUserNotFound()
	{
		Guid userId = Guid.NewGuid();
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByIdAsync($"{userId}"))
			.Returns(Task.FromResult<UserModel?>(null));

		ErrorOr<UserResponse> result = await sut.GetUserById(userId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.UserByIdNotFound(userId));
			_userServiceMock.Verify(x => x.FindByIdAsync($"{userId}"), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), userId, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.GetUserById))]
	public async Task GetUserByIdShouldReturnResultWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		UserModel user = CreateUser(userId);
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.FindByIdAsync($"{userId}"))
			.Returns(Task.FromResult<UserModel?>(user));

		ErrorOr<UserResponse> result = await sut.GetUserById(userId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Id.Should().Be(user.Id);
			result.Value.FirstName.Should().Be(user.FirstName);
			result.Value.MiddleName.Should().Be(user.MiddleName);
			result.Value.LastName.Should().Be(user.LastName);
			result.Value.DateOfBirth.Should().Be(user.DateOfBirth);
			result.Value.Email.Should().Be(user.Email);
			result.Value.PhoneNumber.Should().Be(user.PhoneNumber);
			result.Value.Picture.Should().Be(Convert.ToBase64String(user.Picture ?? []));
			result.Value.Preferences.Should().Be(user.Preferences);
			_userServiceMock.Verify(x => x.FindByIdAsync($"{userId}"), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), userId, It.IsAny<Exception>()), Times.Never);
		});
	}
}
