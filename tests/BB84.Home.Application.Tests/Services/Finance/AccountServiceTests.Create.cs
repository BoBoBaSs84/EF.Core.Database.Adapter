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
public sealed partial class AccountServiceTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(AccountService.Create))]
	public async Task CreateShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		AccountCreateRequest request = RequestHelper.GetAccountCreateRequest();
		AccountService sut = CreateMockedInstance();

		ErrorOr<Created> result = await sut.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.CreateAccountFailed(request.IBAN));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.Create))]
	public async Task CreateShouldReturnConflictWhenAccountNumberAlreadyExists()
	{
		Guid userId = Guid.NewGuid();
		AccountEntity model = new();
		AccountCreateRequest request = RequestHelper.GetAccountCreateRequest();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<AccountEntity, bool>>>(), null, false, false, default))
			.Returns(Task.FromResult<AccountEntity?>(model));
		AccountService sut = CreateMockedInstance(accountMock.Object);

		ErrorOr<Created> result = await sut.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.CreateAccountNumberConflict(request.IBAN));
			accountMock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<AccountEntity, bool>>>(), null, false, false, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.Create))]
	public async Task CreateShouldReturnConflictWhenCardNumberAlreadyExists()
	{
		Guid userId = Guid.NewGuid();
		AccountEntity accountModel = new();
		CardEntity cardModel = new();
		AccountCreateRequest accountRequest = RequestHelper.GetAccountCreateRequest();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<AccountEntity, bool>>>(), null, false, false, default))
			.Returns(Task.FromResult<AccountEntity?>(null));
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, false, default))
			.Returns(Task.FromResult<CardEntity?>(cardModel));
		AccountService sut = CreateMockedInstance(accountMock.Object, cardMock.Object);

		ErrorOr<Created> result = await sut.Create(userId, accountRequest);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.CreateCardNumberConflict(accountRequest.Cards!.First().PAN));
			accountMock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<AccountEntity, bool>>>(), null, false, false, default), Times.Once);
			cardMock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, false, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), accountRequest, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.Create))]
	public async Task CreateShouldReturnCreatedWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		AccountCreateRequest accountRequest = RequestHelper.GetAccountCreateRequest();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<AccountEntity, bool>>>(), null, false, false, default))
			.Returns(Task.FromResult<AccountEntity?>(null));
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, false, default))
			.Returns(Task.FromResult<CardEntity?>(null));
		AccountService sut = CreateMockedInstance(accountMock.Object, cardMock.Object);

		ErrorOr<Created> result = await sut.Create(userId, accountRequest);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			accountMock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<AccountEntity, bool>>>(), null, false, false, default), Times.Once);
			cardMock.Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, false, default), Times.Once);
			accountMock.Verify(x => x.CreateAsync(It.IsAny<AccountEntity>(), default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), accountRequest, It.IsAny<Exception>()), Times.Never);
		});
	}
}
