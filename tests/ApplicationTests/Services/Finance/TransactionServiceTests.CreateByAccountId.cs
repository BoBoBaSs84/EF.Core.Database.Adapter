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
	[TestCategory(nameof(TransactionService.CreateByAccountId))]
	public async Task CreateByAccountIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		TransactionCreateRequest request = new();
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
		TransactionCreateRequest request = new();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(id, false, false, default))
			.Returns(Task.FromResult<AccountModel?>(null));
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
		TransactionCreateRequest request = new();
		AccountModel model = new();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(id, false, false, default))
			.Returns(Task.FromResult<AccountModel?>(model));
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
			transactionMock.Verify(x => x.CreateAsync(It.IsAny<TransactionModel>(), default));
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
