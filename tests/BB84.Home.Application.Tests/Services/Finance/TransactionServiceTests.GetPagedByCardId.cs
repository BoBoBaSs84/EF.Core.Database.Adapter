using BB84.EntityFrameworkCore.Repositories.Abstractions;
using BB84.Home.Application.Contracts.Responses.Finance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Features.Requests;
using BB84.Home.Application.Features.Responses;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Finance;
using BB84.Home.Application.Services.Finance;
using BB84.Home.Application.Tests;
using BB84.Home.Base.Tests.Helpers;
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
	[TestCategory(nameof(TransactionService.GetPagedByCardId))]
	public async Task GetPagedByCardIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		TransactionService sut = CreateMockedInstance();

		ErrorOr<IPagedList<TransactionResponse>> result = await sut
			.GetPagedByCardId(id, new(), _cancellationToken)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TransactionServiceErrors.GetPagedByCardIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TransactionService.GetPagedByCardId))]
	public async Task GetPagedByCardIdShouldReturnResponseWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		TransactionParameters parameters = new();
		IReadOnlyList<TransactionEntity> transactionEntities = [];
		Mock<ITransactionRepository> mock = new();
		mock.Setup(x => x.GetListAsync(It.IsAny<Query<TransactionEntity>>(), _cancellationToken))
			.ReturnsAsync(transactionEntities);
		mock.Setup(x => x.CountAsync(It.IsAny<Query<TransactionEntity>>(), _cancellationToken))
			.ReturnsAsync(transactionEntities.Count);

		TransactionService sut = CreateMockedInstance(transactionRepository: mock.Object);

		ErrorOr<IPagedList<TransactionResponse>> result = await sut
			.GetPagedByCardId(id, parameters, _cancellationToken)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().HaveCount(0);
			mock.Verify(x => x.GetListAsync(It.IsAny<Query<TransactionEntity>>(), _cancellationToken), Times.Once);
			mock.Verify(x => x.CountAsync(It.IsAny<Query<TransactionEntity>>(), _cancellationToken), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
