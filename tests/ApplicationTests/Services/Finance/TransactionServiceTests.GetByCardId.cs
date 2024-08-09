﻿using System.Linq.Expressions;

using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Services.Finance;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Models.Finance;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Finance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class TransactionServiceTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(TransactionService.GetByCardId))]
	public async Task GetByCardIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid cardId = Guid.NewGuid(), id = Guid.NewGuid();
		string[] parameters = [$"{cardId}", $"{id}"];
		TransactionService sut = CreateMockedInstance();

		ErrorOr<TransactionResponse> result = await sut.GetByCardId(cardId, id)
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
	[TestCategory(nameof(TransactionService.GetByCardId))]
	public async Task GetByCardIdShouldReturnNotFoundWhenTransactionDoesNotExists()
	{
		Guid cardId = Guid.NewGuid(), id = Guid.NewGuid();
		Mock<ITransactionRepository> transactionMock = new();
		transactionMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TransactionModel, bool>>>(), null, false, false, default))
			.Returns(Task.FromResult<TransactionModel?>(null));
		TransactionService sut = CreateMockedInstance(transactionRepository: transactionMock.Object);

		ErrorOr<TransactionResponse> result = await sut.GetByCardId(cardId, id)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TransactionServiceErrors.GetByIdNotFound(id));
			transactionMock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TransactionModel, bool>>>(), null, false, false, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TransactionService.GetByCardId))]
	public async Task GetByCardIdShouldReturnTransactionResponseWhenSuccessful()
	{
		Guid cardId = Guid.NewGuid(), id = Guid.NewGuid();
		TransactionModel model = CreateTransaction();
		Mock<ITransactionRepository> transactionMock = new();
		transactionMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TransactionModel, bool>>>(), null, false, false, default))
			.Returns(Task.FromResult<TransactionModel?>(model));
		TransactionService sut = CreateMockedInstance(transactionRepository: transactionMock.Object);

		ErrorOr<TransactionResponse> result = await sut.GetByCardId(cardId, id)
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
			transactionMock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TransactionModel, bool>>>(), null, false, false, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
