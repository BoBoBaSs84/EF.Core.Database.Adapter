using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Tests;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Finance;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Finance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AccountServiceTests : ApplicationTestBase
{
	[TestMethod]
	public async Task DeleteAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;

		ErrorOr<Deleted> result = await _sut
			.DeleteAsync(id, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.DeleteAccountFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task DeleteAsyncShouldReturnNotFoundWhenAccountNotFound()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(id, default, default, token))
			.Returns(Task.FromResult<AccountEntity?>(null));
		_repositoryServiceMock.Setup(x => x.AccountRepository)
			.Returns(accountMock.Object);

		ErrorOr<Deleted> result = await _sut
			.DeleteAsync(id, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.DeleteAccountNotFound(id));
			_repositoryServiceMock.Verify(x => x.AccountRepository.GetByIdAsync(id, default, default, token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task DeleteAsyncShouldReturnDeletedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		AccountEntity account = new();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(id, default, default, token))
			.Returns(Task.FromResult<AccountEntity?>(account));
		accountMock.Setup(x => x.DeleteAsync(account, token))
			.Returns(Task.CompletedTask);
		_repositoryServiceMock.Setup(x => x.AccountRepository)
			.Returns(accountMock.Object);
		_repositoryServiceMock.Setup(x => x.CommitChangesAsync(token))
			.Returns(Task.FromResult(1));

		ErrorOr<Deleted> result = await _sut
			.DeleteAsync(id, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
			_repositoryServiceMock.Verify(x => x.AccountRepository.GetByIdAsync(id, default, default, token), Times.Once);
			_repositoryServiceMock.Verify(x => x.AccountRepository.DeleteAsync(account, token), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
