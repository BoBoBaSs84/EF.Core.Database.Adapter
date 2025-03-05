using System.Linq.Expressions;

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
	[TestCategory(nameof(TransactionService.UpdateByCardId))]
	public async Task UpdateByCardIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid cardId = Guid.NewGuid(), id = Guid.NewGuid();
		TransactionUpdateRequest request = RequestHelper.GetTransactionUpdateRequest();
		string[] parameters = [$"{cardId}", $"{id}"];
		TransactionService sut = CreateMockedInstance();

		ErrorOr<Updated> result = await sut.UpdateByCardId(cardId, id, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TransactionServiceErrors.UpdateByCardIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TransactionService.UpdateByCardId))]
	public async Task UpdateByCardIdShouldReturnNotFoundWhenTransactionNotFound()
	{
		Guid cardId = Guid.NewGuid(), id = Guid.NewGuid();
		TransactionUpdateRequest request = RequestHelper.GetTransactionUpdateRequest();
		string[] parameters = [$"{cardId}", $"{id}"];
		Mock<ITransactionRepository> mock = new();
		mock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TransactionEntity, bool>>>(), null, false, true, default))
			.Returns(Task.FromResult<TransactionEntity?>(null));
		TransactionService sut = CreateMockedInstance(transactionRepository: mock.Object);

		ErrorOr<Updated> result = await sut.UpdateByCardId(cardId, id, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TransactionServiceErrors.UpdateByCardIdNotFound(id));
			mock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TransactionEntity, bool>>>(), null, false, true, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TransactionService.UpdateByCardId))]
	public async Task UpdateByCardIdShouldReturnUpdatedWhenSuccessful()
	{
		Guid cardId = Guid.NewGuid(), id = Guid.NewGuid();
		TransactionUpdateRequest request = RequestHelper.GetTransactionUpdateRequest();
		TransactionEntity model = new() { Id = id };
		string[] parameters = [$"{cardId}", $"{id}"];
		Mock<ITransactionRepository> mock = new();
		mock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TransactionEntity, bool>>>(), null, false, true, default))
			.Returns(Task.FromResult<TransactionEntity?>(model));
		TransactionService sut = CreateMockedInstance(transactionRepository: mock.Object);

		ErrorOr<Updated> result = await sut.UpdateByCardId(cardId, id, request)
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
			mock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TransactionEntity, bool>>>(), null, false, true, default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Never);
		});
	}
}
