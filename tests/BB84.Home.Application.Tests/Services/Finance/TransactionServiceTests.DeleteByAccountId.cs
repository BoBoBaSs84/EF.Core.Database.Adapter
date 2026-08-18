using BB84.EntityFrameworkCore.Repositories.Abstractions;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Finance;
using BB84.Home.Application.Services.Finance;
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
public sealed partial class TransactionServiceTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(TransactionService.DeleteByAccountId))]
	public async Task DeleteByAccountIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid accountId = Guid.NewGuid(), id = Guid.NewGuid();
		string[] parameters = [$"{accountId}", $"{id}"];
		TransactionService sut = CreateMockedInstance();

		ErrorOr<Deleted> result = await sut.DeleteByAccountId(accountId, id, _cancellationToken)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TransactionServiceErrors.DeleteByAccountIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TransactionService.DeleteByAccountId))]
	public async Task DeleteByAccountIdShouldReturnNotFoundWhenCardNotFound()
	{
		Guid accountId = Guid.NewGuid(), id = Guid.NewGuid();
		Mock<ITransactionRepository> transactionMock = new();
		transactionMock.Setup(x => x.GetSingleAsync(It.IsAny<Query<TransactionEntity>>(), default))
			.Returns(Task.FromResult<TransactionEntity?>(null));
		TransactionService sut = CreateMockedInstance(transactionRepository: transactionMock.Object);

		ErrorOr<Deleted> result = await sut.DeleteByAccountId(accountId, id, _cancellationToken)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TransactionServiceErrors.DeleteByAccountIdNotFound(id));
			transactionMock.Verify(x => x.GetSingleAsync(It.IsAny<Query<TransactionEntity>>(), default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TransactionService.DeleteByAccountId))]
	public async Task DeleteByAccountIdShouldReturnDeletedWhenSuccessful()
	{
		Guid accountId = Guid.NewGuid(), id = Guid.NewGuid();
		TransactionEntity transaction = new();
		Mock<ITransactionRepository> transactionMock = new();
		transactionMock.Setup(x => x.GetSingleAsync(It.IsAny<Query<TransactionEntity>>(), default))
			.Returns(Task.FromResult<TransactionEntity?>(transaction));
		TransactionService sut = CreateMockedInstance(transactionRepository: transactionMock.Object);
		_repositoryServiceMock.Setup(x => x.CommitChangesAsync(default))
			.Returns(Task.FromResult(1));

		ErrorOr<Deleted> result = await sut.DeleteByAccountId(accountId, id, _cancellationToken)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
			transactionMock.Verify(x => x.GetSingleAsync(It.IsAny<Query<TransactionEntity>>(), default), Times.Once);
			transactionMock.Verify(x => x.Delete(transaction), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
