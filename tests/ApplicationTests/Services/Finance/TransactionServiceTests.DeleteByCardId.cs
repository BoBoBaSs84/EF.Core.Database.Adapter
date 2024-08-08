using System.Linq.Expressions;

using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Services.Finance;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Models.Finance;
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
		transactionMock.Setup(x => x.DeleteAsync(It.IsAny<Expression<Func<TransactionModel, bool>>>(), default))
			.Returns(Task.FromResult(0));
		TransactionService sut = CreateMockedInstance(transactionRepository: transactionMock.Object);

		ErrorOr<Deleted> result = await sut.DeleteByCardId(cardId, id, default)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TransactionServiceErrors.DeleteByCardIdNotFound(id));
			transactionMock.Verify(x => x.DeleteAsync(It.IsAny<Expression<Func<TransactionModel, bool>>>(), default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TransactionService.DeleteByCardId))]
	public async Task DeleteByCardIdShouldReturnDeletedWhenSuccessful()
	{
		Guid accountId = Guid.NewGuid(), id = Guid.NewGuid();
		Mock<ITransactionRepository> transactionMock = new();
		transactionMock.Setup(x => x.DeleteAsync(It.IsAny<Expression<Func<TransactionModel, bool>>>(), default))
			.Returns(Task.FromResult(1));
		TransactionService sut = CreateMockedInstance(transactionRepository: transactionMock.Object);

		ErrorOr<Deleted> result = await sut.DeleteByCardId(accountId, id, default)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
			transactionMock.Verify(x => x.DeleteAsync(It.IsAny<Expression<Func<TransactionModel, bool>>>(), default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
