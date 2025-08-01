using System.Linq.Expressions;

using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Services.Finance;
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
public sealed partial class CardServiceTests : ApplicationTestBase
{
	[TestMethod]
	public async Task CreateAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid(), accountId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		string[] parameters = [$"{userId}", $"{accountId}"];
		CardCreateRequest request = RequestHelper.GetCardCreateRequest();
		CardService sut = CreateMockedInstance();
		_currentUserServiceMock.Setup(x => x.UserId)
			.Returns(userId);

		ErrorOr<Created> result = await _sut
			.CreateAsync(accountId, request, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.CreateFailed(accountId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task CreateAsyncShouldReturnNotFoundWhenAccountIsNotFound()
	{
		Guid userId = Guid.NewGuid(), accountId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		CardCreateRequest request = RequestHelper.GetCardCreateRequest();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(accountId, false, false, token))
			.Returns(Task.FromResult<AccountEntity?>(null));
		_repositoryServiceMock.Setup(x => x.AccountRepository)
			.Returns(accountMock.Object);

		ErrorOr<Created> result = await _sut
			.CreateAsync(accountId, request, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.CreateAccountIdNotFound(accountId));
			accountMock.Verify(x => x.GetByIdAsync(accountId, false, false, token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task CreateAsyncShouldReturnConflictWhenCardNumberIsFound()
	{
		Guid userId = Guid.NewGuid(), accountId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		CardCreateRequest request = RequestHelper.GetCardCreateRequest();
		AccountEntity accountModel = new();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(accountId, false, false, token))
			.Returns(Task.FromResult<AccountEntity?>(accountModel));
		CardEntity cardModel = new() { PAN = request.PAN };
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByConditionAsync(x => x.PAN == request.PAN, null, false, false, token))
			.Returns(Task.FromResult<CardEntity?>(cardModel));
		_repositoryServiceMock.Setup(x => x.AccountRepository)
			.Returns(accountMock.Object);
		_repositoryServiceMock.Setup(x => x.CardRepository)
			.Returns(cardMock.Object);

		ErrorOr<Created> result = await _sut
			.CreateAsync(accountId, request, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.CreateNumberConflict(request.PAN));
			accountMock.Verify(x => x.GetByIdAsync(accountId, false, false, token), Times.Once);
			cardMock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, false, token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task CreateAsyncShouldReturnCreatedWhenCreateIsSuccessful()
	{
		Guid userId = Guid.NewGuid(), accountId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		CardCreateRequest request = RequestHelper.GetCardCreateRequest();
		AccountEntity accountModel = new();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(accountId, false, false, token))
			.Returns(Task.FromResult<AccountEntity?>(accountModel));
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, false, token))
			.Returns(Task.FromResult<CardEntity?>(null));
		_repositoryServiceMock.Setup(x => x.AccountRepository)
			.Returns(accountMock.Object);
		_repositoryServiceMock.Setup(x => x.CardRepository)
			.Returns(cardMock.Object);
		_repositoryServiceMock.Setup(x => x.CommitChangesAsync(token))
			.Returns(Task.FromResult(1));

		ErrorOr<Created> result = await _sut
			.CreateAsync(accountId, request, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			accountMock.Verify(x => x.GetByIdAsync(accountId, false, false, token), Times.Once);
			cardMock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, false, token), Times.Once);
			cardMock.Verify(x => x.CreateAsync(It.IsAny<CardEntity>(), token), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
