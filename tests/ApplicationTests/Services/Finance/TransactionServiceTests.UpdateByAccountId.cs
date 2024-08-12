﻿using System.Linq.Expressions;

using Application.Contracts.Requests.Finance;
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
	[TestCategory(nameof(TransactionService.UpdateByAccountId))]
	public async Task UpdateByAccountIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid accountId = Guid.NewGuid(), id = Guid.NewGuid();
		TransactionUpdateRequest request = new();
		string[] parameters = [$"{accountId}", $"{id}"];
		TransactionService sut = CreateMockedInstance();

		ErrorOr<Updated> result = await sut.UpdateByAccountId(accountId, id, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TransactionServiceErrors.UpdateByAccountIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TransactionService.UpdateByAccountId))]
	public async Task UpdateByAccountIdShouldReturnNotFoundWhenTransactionNotFound()
	{
		Guid accountId = Guid.NewGuid(), id = Guid.NewGuid();
		TransactionUpdateRequest request = new();
		string[] parameters = [$"{accountId}", $"{id}"];
		Mock<ITransactionRepository> mock = new();
		mock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TransactionModel, bool>>>(), null, false, true, default))
			.Returns(Task.FromResult<TransactionModel?>(null));
		TransactionService sut = CreateMockedInstance(transactionRepository: mock.Object);

		ErrorOr<Updated> result = await sut.UpdateByAccountId(accountId, id, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TransactionServiceErrors.UpdateByAccountIdNotFound(id));
			mock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TransactionModel, bool>>>(), null, false, true, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TransactionService.UpdateByAccountId))]
	public async Task UpdateByAccountIdShouldReturnUpdatedWhenSuccessful()
	{
		Guid accountId = Guid.NewGuid(), id = Guid.NewGuid();
		TransactionUpdateRequest request = CreateUpdateRequest();
		TransactionModel model = new() { Id = id };
		string[] parameters = [$"{accountId}", $"{id}"];
		Mock<ITransactionRepository> mock = new();
		mock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TransactionModel, bool>>>(), null, false, true, default))
			.Returns(Task.FromResult<TransactionModel?>(model));
		TransactionService sut = CreateMockedInstance(transactionRepository: mock.Object);

		ErrorOr<Updated> result = await sut.UpdateByAccountId(accountId, id, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			model.BookingDate.Should().Be(request.BookingDate);
			model.ValueDate.Should().Be(request.ValueDate);
			model.PostingText.Should().Be(request.PostingText);
			model.ClientBeneficiary.Should().Be(request.ClientBeneficiary);
			model.Purpose.Should().Be(request.Purpose);
			model.AccountNumber.Should().Be(request.AccountNumber);
			model.BankCode.Should().Be(request.BankCode);
			model.AmountEur.Should().Be(request.AmountEur);
			model.CreditorId.Should().Be(request.CreditorId);
			model.MandateReference.Should().Be(request.MandateReference);
			model.CustomerReference.Should().Be(request.CustomerReference);
			mock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TransactionModel, bool>>>(), null, false, true, default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Never);
		});
	}
}