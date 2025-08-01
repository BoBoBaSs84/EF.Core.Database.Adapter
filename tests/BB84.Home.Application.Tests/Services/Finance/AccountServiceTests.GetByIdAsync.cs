using System.Linq.Expressions;

using BB84.Home.Application.Contracts.Responses.Finance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Tests;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Finance;
using BB84.Home.Domain.Enumerators.Finance;
using BB84.Home.Domain.Errors;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Finance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AccountServiceTests : ApplicationTestBase
{
	[TestMethod]
	public async Task GetByIdAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;

		ErrorOr<AccountResponse> result = await _sut
			.GetByIdAsync(id, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.GetByIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task GetByIdAsyncShouldReturnNotFoundWhenAccountNotFound()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(id, false, false, token, nameof(AccountEntity.Cards)))
			.Returns(Task.FromResult<AccountEntity?>(null));
		_repositoryServiceMock.Setup(x => x.AccountRepository)
			.Returns(accountMock.Object);

		ErrorOr<AccountResponse> result = await _sut
			.GetByIdAsync(id, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.GetByIdNotFound(id));
			accountMock.Verify(x => x.GetByIdAsync(id, false, false, token, It.IsAny<string[]>()), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task GetByIdAsyncShouldReturnResponseWithNoCardsWhenCardsNotFound()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		AccountEntity accountModel = new() { Id = id, IBAN = "UnitTest", Type = AccountType.Checking, Provider = "UnitTest" };
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(id, false, false, token, nameof(AccountEntity.Cards)))
			.Returns(Task.FromResult<AccountEntity?>(accountModel));
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetManyByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, null, null, null, false, token))
			.Returns(Task.FromResult<IEnumerable<CardEntity>>([]));
		_repositoryServiceMock.Setup(x => x.AccountRepository)
			.Returns(accountMock.Object);
		_repositoryServiceMock.Setup(x => x.CardRepository)
			.Returns(cardMock.Object);

		ErrorOr<AccountResponse> result = await _sut
			.GetByIdAsync(id, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
			result.Value.Id.Should().Be(accountModel.Id);
			result.Value.IBAN.Should().Be(accountModel.IBAN);
			result.Value.Type.Should().Be(accountModel.Type);
			result.Value.Provider.Should().Be(accountModel.Provider);
			result.Value.Cards.Should().BeNull();
			accountMock.Verify(x => x.GetByIdAsync(id, false, false, token, It.IsAny<string[]>()), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task GetByIdAsyncShouldReturnResponseWithCardsWhenCardsFound()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		CardEntity cardModel = new();
		AccountEntity accountModel = new() { Cards = [cardModel] };
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(id, false, false, token, nameof(AccountEntity.Cards)))
			.Returns(Task.FromResult<AccountEntity?>(accountModel));
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetManyByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, null, null, null, false, token))
			.Returns(Task.FromResult<IEnumerable<CardEntity>>([cardModel]));
		_repositoryServiceMock.Setup(x => x.AccountRepository)
			.Returns(accountMock.Object);
		_repositoryServiceMock.Setup(x => x.CardRepository)
			.Returns(cardMock.Object);

		ErrorOr<AccountResponse> result = await _sut
			.GetByIdAsync(id, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
			result.Value.Cards.Should().NotBeNullOrEmpty();
			accountMock.Verify(x => x.GetByIdAsync(id, false, false, token, It.IsAny<string[]>()), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
