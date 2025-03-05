using System.Linq.Expressions;

using BaseTests.Helpers;

using BB84.Home.Application.Contracts.Responses.Finance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Features.Requests;
using BB84.Home.Application.Features.Responses;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Services.Finance;
using BB84.Home.Application.Tests;
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
	[TestCategory(nameof(TransactionService.GetPagedByAccountId))]
	public async Task GetPagedByAccountIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		TransactionService sut = CreateMockedInstance();

		ErrorOr<IPagedList<TransactionResponse>> result = await sut.GetPagedByAccountId(id, new())
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TransactionServiceErrors.GetPagedByAccountIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TransactionService.GetPagedByAccountId))]
	public async Task GetPagedByAccountIdShouldReturnResponseWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		TransactionParameters parameters = new();
		Mock<ITransactionRepository> mock = new();
		TransactionService sut = CreateMockedInstance(transactionRepository: mock.Object);

		ErrorOr<IPagedList<TransactionResponse>> result = await sut.GetPagedByAccountId(id, parameters)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().HaveCount(0);
			mock.Verify(x => x.GetManyByConditionAsync(It.IsAny<Expression<Func<TransactionEntity, bool>>>(), It.IsAny<Func<IQueryable<TransactionEntity>, IQueryable<TransactionEntity>>>(), false, It.IsAny<Func<IQueryable<TransactionEntity>, IOrderedQueryable<TransactionEntity>>>(), (parameters.PageNumber - 1) * parameters.PageSize, parameters.PageSize, false, default), Times.Once);
			mock.Verify(x => x.CountAsync(It.IsAny<Expression<Func<TransactionEntity, bool>>>(), It.IsAny<Func<IQueryable<TransactionEntity>, IQueryable<TransactionEntity>>>(), false, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
