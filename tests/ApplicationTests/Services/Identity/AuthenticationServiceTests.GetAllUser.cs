using BB84.Home.Application.Contracts.Responses.Identity;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Services.Identity;
using BB84.Home.Application.Tests;
using BB84.Home.BaseTests.Helpers;
using BB84.Home.Domain.Entities.Identity;
using BB84.Home.Domain.Enumerators;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Extensions;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Identity;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AuthenticationServiceTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(AuthenticationService.GetAllUser))]
	public async Task GetAllUserShouldReturnFailedWhenExceptionIsThrown()
	{
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.GetUsersInRoleAsync(RoleType.USER.GetName()))
			.Throws(new InvalidOperationException());

		ErrorOr<IEnumerable<UserResponse>> result = await sut.GetAllUser()
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AuthenticationServiceErrors.GetAllFailed);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, Exception?>>(), It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AuthenticationService.GetAllUser))]
	public async Task GetAllUserShouldReturnResultWhenSuccessful()
	{
		List<UserEntity> users = [CreateUser(), CreateUser()];
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.GetUsersInRoleAsync(RoleType.USER.GetName()))
			.Returns(Task.FromResult<IList<UserEntity>>(users));

		ErrorOr<IEnumerable<UserResponse>> result = await sut.GetAllUser()
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().HaveCount(users.Count);
			_userServiceMock.Verify(x => x.GetUsersInRoleAsync(RoleType.USER.GetName()), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, Exception?>>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
