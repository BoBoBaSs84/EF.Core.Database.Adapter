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
	[TestCategory(nameof(TransactionService.CreateByAccountId))]
	public async Task CreateByAccountIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		TransactionCreateRequest request = RequestHelper.GetTransactionCreateRequest();
		TransactionService sut = CreateMockedInstance();

		ErrorOr<Created> result = await sut.CreateByAccountId(id, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TransactionServiceErrors.CreateByAccountIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TransactionService.CreateByAccountId))]
	public async Task CreateByAccountIdShouldReturnNotFoundWhenAccountIsNotFound()
	{
		Guid id = Guid.NewGuid();
		TransactionCreateRequest request = RequestHelper.GetTransactionCreateRequest();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(id, false, false, default))
			.Returns(Task.FromResult<AccountEntity?>(null));
		TransactionService sut = CreateMockedInstance(accountMock.Object);

		ErrorOr<Created> result = await sut.CreateByAccountId(id, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TransactionServiceErrors.CreateByAccountIdNotFound(id));
			accountMock.Verify(x => x.GetByIdAsync(id, false, false, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TransactionService.CreateByAccountId))]
	public async Task CreateByAccountIdShouldReturnCreatedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		TransactionCreateRequest request = RequestHelper.GetTransactionCreateRequest();
		AccountEntity model = new();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(id, false, false, default))
			.Returns(Task.FromResult<AccountEntity?>(model));
		Mock<ITransactionRepository> transactionMock = new();
		TransactionService sut = CreateMockedInstance(accountMock.Object, null, transactionMock.Object);

		ErrorOr<Created> result = await sut.CreateByAccountId(id, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			accountMock.Verify(x => x.GetByIdAsync(id, false, false, default), Times.Once);
			transactionMock.Verify(x => x.CreateAsync(It.IsAny<TransactionEntity>(), default));
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
