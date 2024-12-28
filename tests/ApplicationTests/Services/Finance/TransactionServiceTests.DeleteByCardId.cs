using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Services.Finance;

using BaseTests.Helpers;

using Domain.Entities.Finance;
using Domain.Errors;
using Domain.Results;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Finance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class TransactionServiceTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(TransactionService.DeleteByCardId))]
	public async Task DeleteByCardIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid cardId = Guid.NewGuid(), id = Guid.NewGuid();
		string[] parameters = [$"{cardId}", $"{id}"];
		TransactionService sut = CreateMockedInstance();

		ErrorOr<Deleted> result = await sut.DeleteByCardId(cardId, id, default)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TransactionServiceErrors.DeleteByCardIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TransactionService.DeleteByCardId))]
	public async Task DeleteByCardIdShouldReturnNotFoundWhenCardNotFound()
	{
		Guid cardId = Guid.NewGuid(), id = Guid.NewGuid();
		Mock<ITransactionRepository> transactionMock = new();
		transactionMock.Setup(x => x.GetByConditionAsync(x => x.Id.Equals(id) && x.CardTransactions.Select(x => x.CardId).Contains(cardId), default, default, default, default))
			.Returns(Task.FromResult<TransactionEntity?>(null));
		TransactionService sut = CreateMockedInstance(transactionRepository: transactionMock.Object);

		ErrorOr<Deleted> result = await sut.DeleteByCardId(cardId, id, default)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TransactionServiceErrors.DeleteByCardIdNotFound(id));
			transactionMock.Verify(x => x.GetByConditionAsync(x => x.Id.Equals(id) && x.CardTransactions.Select(x => x.CardId).Contains(cardId), default, default, default, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TransactionService.DeleteByCardId))]
	public async Task DeleteByCardIdShouldReturnDeletedWhenSuccessful()
	{
		Guid cardId = Guid.NewGuid(), id = Guid.NewGuid();
		TransactionEntity transaction = new();
		Mock<ITransactionRepository> transactionMock = new();
		transactionMock.Setup(x => x.GetByConditionAsync(x => x.Id.Equals(id) && x.CardTransactions.Select(x => x.CardId).Contains(cardId), default, default, default, default))
			.Returns(Task.FromResult<TransactionEntity?>(transaction));
		transactionMock.Setup(x => x.DeleteAsync(transaction, default))
			.Returns(Task.CompletedTask);
		TransactionService sut = CreateMockedInstance(transactionRepository: transactionMock.Object);
		_repositoryServiceMock.Setup(x => x.CommitChangesAsync(default))
			.Returns(Task.FromResult(1));

		ErrorOr<Deleted> result = await sut.DeleteByCardId(cardId, id, default)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
			transactionMock.Verify(x => x.GetByConditionAsync(x => x.Id.Equals(id) && x.CardTransactions.Select(x => x.CardId).Contains(cardId), default, default, default, default), Times.Once);
			transactionMock.Verify(x => x.DeleteAsync(transaction, default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
