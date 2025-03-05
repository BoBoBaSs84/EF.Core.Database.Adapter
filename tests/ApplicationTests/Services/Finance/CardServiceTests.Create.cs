using System.Linq.Expressions;

using Application.Contracts.Requests.Finance;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Services.Finance;

using ApplicationTests.Helpers;

using BaseTests.Helpers;

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
	[TestCategory(nameof(CardService.Create))]
	public async Task CreateShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid(), accountId = Guid.NewGuid();
		string[] parameters = [$"{userId}", $"{accountId}"];
		CardCreateRequest request = RequestHelper.GetCardCreateRequest();
		CardService sut = CreateMockedInstance();

		ErrorOr<Created> result = await sut.Create(userId, accountId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.CreateFailed(accountId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.Create))]
	public async Task CreateShouldReturnNotFoundWhenAccountIsNotFound()
	{
		Guid userId = Guid.NewGuid(), accountId = Guid.NewGuid();
		CardCreateRequest request = RequestHelper.GetCardCreateRequest();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(accountId, false, false, default))
			.Returns(Task.FromResult<AccountEntity?>(null));
		CardService sut = CreateMockedInstance(accountMock.Object);

		ErrorOr<Created> result = await sut.Create(userId, accountId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.CreateAccountIdNotFound(accountId));
			accountMock.Verify(x => x.GetByIdAsync(accountId, false, false, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.Create))]
	public async Task CreateShouldReturnConflictWhenCardNumberIsFound()
	{
		Guid userId = Guid.NewGuid(), accountId = Guid.NewGuid();
		CardCreateRequest request = RequestHelper.GetCardCreateRequest();
		AccountEntity accountModel = new();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(accountId, false, false, default))
			.Returns(Task.FromResult<AccountEntity?>(accountModel));
		CardEntity cardModel = new() { PAN = request.PAN };
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, false, default))
			.Returns(Task.FromResult<CardEntity?>(cardModel));
		CardService sut = CreateMockedInstance(accountMock.Object, cardMock.Object);

		ErrorOr<Created> result = await sut.Create(userId, accountId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.CreateNumberConflict(request.PAN));
			accountMock.Verify(x => x.GetByIdAsync(accountId, false, false, default), Times.Once);
			cardMock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, false, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.Create))]
	public async Task CreateShouldReturnCreatedWhenCreateIsSuccessful()
	{
		Guid userId = Guid.NewGuid(), accountId = Guid.NewGuid();
		CardCreateRequest request = RequestHelper.GetCardCreateRequest();
		AccountEntity accountModel = new();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(accountId, false, false, default))
			.Returns(Task.FromResult<AccountEntity?>(accountModel));
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, false, default))
			.Returns(Task.FromResult<CardEntity?>(null));
		CardService sut = CreateMockedInstance(accountMock.Object, cardMock.Object);

		ErrorOr<Created> result = await sut.Create(userId, accountId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			accountMock.Verify(x => x.GetByIdAsync(accountId, false, false, default), Times.Once);
			cardMock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, false, default), Times.Once);
			cardMock.Verify(x => x.CreateAsync(It.IsAny<CardEntity>(), default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
