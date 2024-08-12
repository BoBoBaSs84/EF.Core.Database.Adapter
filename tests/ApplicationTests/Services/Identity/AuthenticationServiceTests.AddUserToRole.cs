using Application.Errors.Services;
using Application.Services.Identity;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Results;

using FluentAssertions;

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
}
