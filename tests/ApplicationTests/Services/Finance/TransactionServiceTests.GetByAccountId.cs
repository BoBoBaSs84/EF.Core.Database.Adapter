using System.Linq.Expressions;

using BB84.Home.Application.Contracts.Responses.Finance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Services.Finance;
using BB84.Home.Application.Tests;
using BB84.Home.BaseTests.Helpers;
using BB84.Home.Domain.Entities.Finance;
using BB84.Home.Domain.Errors;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Finance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class TransactionServiceTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(TransactionService.GetByAccountId))]
	public async Task GetByAccountIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid accountId = Guid.NewGuid(), id = Guid.NewGuid();
		string[] parameters = [$"{accountId}", $"{id}"];
		TransactionService sut = CreateMockedInstance();

		ErrorOr<TransactionResponse> result = await sut.GetByAccountId(accountId, id)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TransactionServiceErrors.GetByIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TransactionService.GetByAccountId))]
	public async Task GetByAccountIdShouldReturnNotFoundWhenTransactionDoesNotExists()
	{
		Guid accountId = Guid.NewGuid(), id = Guid.NewGuid();
		Mock<ITransactionRepository> transactionMock = new();
		transactionMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TransactionEntity, bool>>>(), null, false, false, default))
			.Returns(Task.FromResult<TransactionEntity?>(null));
		TransactionService sut = CreateMockedInstance(transactionRepository: transactionMock.Object);

		ErrorOr<TransactionResponse> result = await sut.GetByAccountId(accountId, id)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TransactionServiceErrors.GetByIdNotFound(id));
			transactionMock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TransactionEntity, bool>>>(), null, false, false, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TransactionService.GetByAccountId))]
	public async Task GetByAccountIdShouldReturnTransactionResponseWhenSuccessful()
	{
		Guid accountId = Guid.NewGuid(), id = Guid.NewGuid();
		TransactionEntity model = new();
		Mock<ITransactionRepository> transactionMock = new();
		transactionMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TransactionEntity, bool>>>(), null, false, false, default))
			.Returns(Task.FromResult<TransactionEntity?>(model));
		TransactionService sut = CreateMockedInstance(transactionRepository: transactionMock.Object);

		ErrorOr<TransactionResponse> result = await sut.GetByAccountId(accountId, id)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Id.Should().Be(model.Id);
			result.Value.BookingDate.Should().Be(model.BookingDate);
			result.Value.ValueDate.Should().Be(model.ValueDate);
			result.Value.PostingText.Should().Be(model.PostingText);
			result.Value.ClientBeneficiary.Should().Be(model.ClientBeneficiary);
			result.Value.Purpose.Should().Be(model.Purpose);
			result.Value.AccountNumber.Should().Be(model.AccountNumber);
			result.Value.BankCode.Should().Be(model.BankCode);
			result.Value.AmountEur.Should().Be(model.AmountEur);
			result.Value.CreditorId.Should().Be(model.CreditorId);
			result.Value.MandateReference.Should().Be(model.MandateReference);
			result.Value.CustomerReference.Should().Be(model.CustomerReference);
			transactionMock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TransactionEntity, bool>>>(), null, false, false, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
