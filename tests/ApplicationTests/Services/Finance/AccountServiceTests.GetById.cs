using System.Linq.Expressions;

using BaseTests.Helpers;

using BB84.Home.Application.Contracts.Responses.Finance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Services.Finance;
using BB84.Home.Application.Tests;
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
	[TestCategory(nameof(AccountService.GetById))]
	public async Task GetByIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		AccountService sut = CreateMockedInstance();

		ErrorOr<AccountResponse> result = await sut.GetById(id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.GetByIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.GetById))]
	public async Task GetByIdShouldReturnNotFoundWhenAccountNotFound()
	{
		Guid id = Guid.NewGuid();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(id, false, false, default, nameof(AccountEntity.Cards)))
			.Returns(Task.FromResult<AccountEntity?>(null));
		AccountService sut = CreateMockedInstance(accountMock.Object);

		ErrorOr<AccountResponse> result = await sut.GetById(id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.GetByIdNotFound(id));
			accountMock.Verify(x => x.GetByIdAsync(id, false, false, default, It.IsAny<string[]>()), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.GetById))]
	public async Task GetByIdShouldReturnResponseWithNoCardsWhenCardsNotFound()
	{
		Guid id = Guid.NewGuid();
		AccountEntity accountModel = new() { Id = id, IBAN = "UnitTest", Type = AccountType.CHECKING, Provider = "UnitTest" };
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(id, false, false, default, nameof(AccountEntity.Cards)))
			.Returns(Task.FromResult<AccountEntity?>(accountModel));
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetManyByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, null, null, null, false, default))
			.Returns(Task.FromResult<IEnumerable<CardEntity>>([]));
		AccountService sut = CreateMockedInstance(accountMock.Object, cardMock.Object);

		ErrorOr<AccountResponse> result = await sut.GetById(id);

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
			accountMock.Verify(x => x.GetByIdAsync(id, false, false, default, It.IsAny<string[]>()), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.GetById))]
	public async Task GetByIdShouldReturnResponseWithCardsWhenCardsFound()
	{
		Guid id = Guid.NewGuid();
		CardEntity cardModel = new();
		AccountEntity accountModel = new() { Cards = [cardModel] };
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(id, false, false, default, nameof(AccountEntity.Cards)))
			.Returns(Task.FromResult<AccountEntity?>(accountModel));
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetManyByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, null, null, null, false, default))
			.Returns(Task.FromResult<IEnumerable<CardEntity>>([cardModel]));
		AccountService sut = CreateMockedInstance(accountMock.Object, cardMock.Object);

		ErrorOr<AccountResponse> result = await sut.GetById(id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
			result.Value.Cards.Should().NotBeNullOrEmpty();
			accountMock.Verify(x => x.GetByIdAsync(id, false, false, default, It.IsAny<string[]>()), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
