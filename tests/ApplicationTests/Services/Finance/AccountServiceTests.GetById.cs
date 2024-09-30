using System.Linq.Expressions;

using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Services.Finance;

using BaseTests.Helpers;

using Domain.Enumerators.Finance;
using Domain.Errors;
using Domain.Models.Finance;

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
		accountMock.Setup(x => x.GetByIdAsync(id, false, false, default))
			.Returns(Task.FromResult<AccountModel?>(null));
		AccountService sut = CreateMockedInstance(accountMock.Object);

		ErrorOr<AccountResponse> result = await sut.GetById(id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.GetByIdNotFound(id));
			accountMock.Verify(x => x.GetByIdAsync(id, false, false, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.GetById))]
	public async Task GetByIdShouldReturnResponseWithNoCardsWhenCardsNotFound()
	{
		Guid id = Guid.NewGuid();
		AccountModel accountModel = new() { Id = id, IBAN = "UnitTest", Type = AccountType.CHECKING, Provider = "UnitTest" };
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(id, false, false, default))
			.Returns(Task.FromResult<AccountModel?>(accountModel));
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetManyByConditionAsync(It.IsAny<Expression<Func<CardModel, bool>>>(), null, false, null, null, null, false, default))
			.Returns(Task.FromResult<IEnumerable<CardModel>>([]));
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
			accountMock.Verify(x => x.GetByIdAsync(id, false, false, default), Times.Once);
			cardMock.Verify(x => x.GetManyByConditionAsync(It.IsAny<Expression<Func<CardModel, bool>>>(), null, false, null, null, null, false, default), Times.Once());
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.GetById))]
	public async Task GetByIdShouldReturnResponseWithCardsWhenCardsFound()
	{
		Guid id = Guid.NewGuid();
		AccountModel accountModel = new();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(id, false, false, default))
			.Returns(Task.FromResult<AccountModel?>(accountModel));
		CardModel cardModel = new() { Id = Guid.NewGuid(), Type = CardType.CREDIT, PAN = "UnitTest", ValidUntil = DateTime.Today };
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetManyByConditionAsync(It.IsAny<Expression<Func<CardModel, bool>>>(), null, false, null, null, null, false, default))
			.Returns(Task.FromResult<IEnumerable<CardModel>>([cardModel]));
		AccountService sut = CreateMockedInstance(accountMock.Object, cardMock.Object);

		ErrorOr<AccountResponse> result = await sut.GetById(id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
			result.Value.Cards.Should().NotBeNullOrEmpty();
			result.Value.Cards?.First().Id.Should().Be(cardModel.Id);
			result.Value.Cards?.First().Type.Should().Be(cardModel.Type);
			result.Value.Cards?.First().PAN.Should().Be(cardModel.PAN);
			result.Value.Cards?.First().ValidUntil.Should().Be(cardModel.ValidUntil);
			accountMock.Verify(x => x.GetByIdAsync(id, false, false, default), Times.Once);
			cardMock.Verify(x => x.GetManyByConditionAsync(It.IsAny<Expression<Func<CardModel, bool>>>(), null, false, null, null, null, false, default), Times.Once());
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
