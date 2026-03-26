using System.Linq.Expressions;

using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Finance;
using BB84.Home.Application.Tests;
using BB84.Home.Application.Tests.Helpers;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Finance;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Finance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AccountServiceTests : ApplicationTestBase
{
	[TestMethod]
	public async Task CreateAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		AccountCreateRequest request = RequestHelper.GetAccountCreateRequest();

		ErrorOr<Created> result = await _sut
			.CreateAsync(request, _cancellationToken)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.CreateAccountFailed(request.IBAN));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task CreateAsyncShouldReturnConflictWhenAccountNumberAlreadyExists()
	{
		Guid userId = Guid.NewGuid();
		AccountEntity model = new();
		AccountCreateRequest request = RequestHelper.GetAccountCreateRequest();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<AccountEntity, bool>>>(), null, false, false, _cancellationToken))
			.Returns(Task.FromResult<AccountEntity?>(model));
		_repositoryServiceMock.Setup(x => x.AccountRepository)
			.Returns(accountMock.Object);

		ErrorOr<Created> result = await _sut
			.CreateAsync(request, _cancellationToken)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.CreateAccountNumberConflict(request.IBAN));
			accountMock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<AccountEntity, bool>>>(), null, false, false, _cancellationToken), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task CreateAsyncShouldReturnConflictWhenCardNumberAlreadyExists()
	{
		Guid userId = Guid.NewGuid();
		AccountEntity accountModel = new();
		CardEntity cardModel = new();
		AccountCreateRequest request = RequestHelper.GetAccountCreateRequest();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<AccountEntity, bool>>>(), null, false, false, _cancellationToken))
			.Returns(Task.FromResult<AccountEntity?>(null));
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, false, _cancellationToken))
			.Returns(Task.FromResult<CardEntity?>(cardModel));
		_repositoryServiceMock.Setup(x => x.AccountRepository)
			.Returns(accountMock.Object);
		_repositoryServiceMock.Setup(x => x.CardRepository)
			.Returns(cardMock.Object);

		ErrorOr<Created> result = await _sut
			.CreateAsync(request, _cancellationToken)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.CreateCardNumberConflict(request.Cards!.First().PAN));
			accountMock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<AccountEntity, bool>>>(), null, false, false, _cancellationToken), Times.Once);
			cardMock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, false, _cancellationToken), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task CreateAsyncShouldReturnCreatedWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		AccountCreateRequest request = RequestHelper.GetAccountCreateRequest();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<AccountEntity, bool>>>(), null, false, false, _cancellationToken))
			.Returns(Task.FromResult<AccountEntity?>(null));
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, false, _cancellationToken))
			.Returns(Task.FromResult<CardEntity?>(null));
		_repositoryServiceMock.Setup(x => x.AccountRepository)
			.Returns(accountMock.Object);
		_repositoryServiceMock.Setup(x => x.CardRepository)
			.Returns(cardMock.Object);

		ErrorOr<Created> result = await _sut
			.CreateAsync(request, _cancellationToken)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			accountMock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<AccountEntity, bool>>>(), null, false, false, _cancellationToken), Times.Once);
			cardMock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, false, _cancellationToken), Times.Once);
			accountMock.Verify(x => x.CreateAsync(It.IsAny<AccountEntity>(), _cancellationToken), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(_cancellationToken), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Never);
		});
	}
}
