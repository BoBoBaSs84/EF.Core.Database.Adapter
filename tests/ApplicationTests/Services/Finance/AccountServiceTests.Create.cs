using System.Linq.Expressions;

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
public sealed partial class AccountServiceTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(AccountService.Create))]
	public async Task CreateShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		AccountCreateRequest request = new();
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
		AccountModel model = new();
		AccountCreateRequest request = new();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<AccountModel, bool>>>(), null, false, false, default))
			.Returns(Task.FromResult<AccountModel?>(model));
		AccountService sut = CreateMockedInstance(accountMock.Object);

		ErrorOr<Created> result = await sut.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.CreateAccountNumberConflict(request.IBAN));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.Create))]
	public async Task CreateShouldReturnConflictWhenCardNumberAlreadyExists()
	{
		Guid userId = Guid.NewGuid();
		AccountModel accountModel = new();
		CardModel cardModel = new();
		CardCreateRequest cardRequest = new();
		AccountCreateRequest accountRequest = new() { Cards = [cardRequest] };
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<AccountModel, bool>>>(), null, false, false, default))
			.Returns(Task.FromResult<AccountModel?>(null));
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<CardModel, bool>>>(), null, false, false, default))
			.Returns(Task.FromResult<CardModel?>(cardModel));
		AccountService sut = CreateMockedInstance(accountMock.Object, cardMock.Object);

		ErrorOr<Created> result = await sut.Create(userId, accountRequest);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.CreateCardNumberConflict(cardRequest.PAN));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), accountRequest, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.Create))]
	public async Task CreateShouldReturnCreatedWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		CardCreateRequest cardRequest = new();
		AccountCreateRequest accountRequest = new() { Cards = [cardRequest] };
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<AccountModel, bool>>>(), null, false, false, default))
			.Returns(Task.FromResult<AccountModel?>(null));
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<CardModel, bool>>>(), null, false, false, default))
			.Returns(Task.FromResult<CardModel?>(null));
		AccountService sut = CreateMockedInstance(accountMock.Object, cardMock.Object);

		ErrorOr<Created> result = await sut.Create(userId, accountRequest);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			accountMock.Verify(x => x.CreateAsync(It.IsAny<AccountModel>(), default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), accountRequest, It.IsAny<Exception>()), Times.Never);
		});
	}
}
