using BaseTests.Helpers;

using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Services.Finance;
using BB84.Home.Application.Tests;
using BB84.Home.Application.Tests.Helpers;
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
	[TestCategory(nameof(TransactionService.CreateByCardId))]
	public async Task CreateByCardIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		TransactionCreateRequest request = RequestHelper.GetTransactionCreateRequest();
		TransactionService sut = CreateMockedInstance();

		ErrorOr<Created> result = await sut.CreateByCardId(id, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TransactionServiceErrors.CreateByCardIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TransactionService.CreateByCardId))]
	public async Task CreateByCardIdShouldReturnNotFoundWhenAccountIsNotFound()
	{
		Guid id = Guid.NewGuid();
		TransactionCreateRequest request = RequestHelper.GetTransactionCreateRequest();
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByIdAsync(id, false, false, default))
			.Returns(Task.FromResult<CardEntity?>(null));
		TransactionService sut = CreateMockedInstance(cardRepository: cardMock.Object);

		ErrorOr<Created> result = await sut.CreateByCardId(id, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TransactionServiceErrors.CreateByCardIdNotFound(id));
			cardMock.Verify(x => x.GetByIdAsync(id, false, false, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TransactionService.CreateByCardId))]
	public async Task CreateByCardIdShouldReturnCreatedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		TransactionCreateRequest request = RequestHelper.GetTransactionCreateRequest();
		CardEntity model = new();
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByIdAsync(id, false, false, default))
			.Returns(Task.FromResult<CardEntity?>(model));
		Mock<ITransactionRepository> transactionMock = new();
		TransactionService sut = CreateMockedInstance(null, cardMock.Object, transactionMock.Object);

		ErrorOr<Created> result = await sut.CreateByCardId(id, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			cardMock.Verify(x => x.GetByIdAsync(id, false, false, default), Times.Once);
			transactionMock.Verify(x => x.CreateAsync(It.IsAny<TransactionEntity>(), default));
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
