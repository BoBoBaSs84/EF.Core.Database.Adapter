using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Services.Finance;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Results;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Finance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AccountServiceTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(AccountService.Delete))]
	public async Task DeleteShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid accountId = Guid.NewGuid();
		AccountService sut = CreateMockedInstance();

		ErrorOr<Deleted> result = await sut.Delete(accountId);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.DeleteAccountFailed(accountId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), accountId, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.Delete))]
	public async Task DeleteShouldReturnNotFoundWhenAccountNotFound()
	{
		Guid accountId = Guid.NewGuid();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.DeleteAsync(accountId, default))
			.Returns(Task.FromResult(0));
		AccountService sut = CreateMockedInstance(accountMock.Object);

		ErrorOr<Deleted> result = await sut.Delete(accountId);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.DeleteAccountNotFound(accountId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), accountId, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.Delete))]
	public async Task DeleteShouldReturnDeletedWhenSuccessful()
	{
		Guid accountId = Guid.NewGuid();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.DeleteAsync(accountId, default))
			.Returns(Task.FromResult(1));
		AccountService sut = CreateMockedInstance(accountMock.Object);

		ErrorOr<Deleted> result = await sut.Delete(accountId);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), accountId, It.IsAny<Exception>()), Times.Never);
		});
	}
}
