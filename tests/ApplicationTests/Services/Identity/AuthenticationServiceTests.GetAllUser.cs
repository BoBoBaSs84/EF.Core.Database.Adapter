using Application.Contracts.Responses.Identity;
using Application.Errors.Services;
using Application.Services.Identity;

using BaseTests.Helpers;

using Domain.Enumerators;
using Domain.Errors;
using Domain.Extensions;
using Domain.Models.Identity;

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
		List<UserModel> users = [new(), new()];
		AuthenticationService sut = CreateMockedInstance();
		_userServiceMock.Setup(x => x.GetUsersInRoleAsync(RoleType.USER.GetName()))
			.Returns(Task.FromResult<IList<UserModel>>(users));

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
