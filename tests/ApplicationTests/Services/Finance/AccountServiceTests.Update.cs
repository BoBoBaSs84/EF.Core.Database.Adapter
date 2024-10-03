using System.Linq.Expressions;

using Application.Contracts.Requests.Finance;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Services.Finance;

using ApplicationTests.Helpers;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Models.Finance;
using Domain.Results;

using FluentAssertions;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Finance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AccountServiceTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(AccountService.Update))]
	public async Task UpdateShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		AccountUpdateRequest request = RequestHelper.GetAccountUpdateRequest();
		AccountService sut = CreateMockedInstance();

		ErrorOr<Updated> result = await sut.Update(id, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.UpdateAccountFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.Update))]
	public async Task UpdateShouldReturnNotFoundWhenAccountNotFound()
	{
		Guid id = Guid.NewGuid();
		AccountUpdateRequest request = RequestHelper.GetAccountUpdateRequest();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.UpdateAsync(id, It.IsAny<Expression<Func<SetPropertyCalls<AccountModel>, SetPropertyCalls<AccountModel>>>>(), default))
			.Returns(Task.FromResult(0));
		AccountService sut = CreateMockedInstance(accountMock.Object);

		ErrorOr<Updated> result = await sut.Update(id, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.UpdateAccountNotFound(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.Update))]
	public async Task UpdateShouldReturnUpdatedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		AccountUpdateRequest request = RequestHelper.GetAccountUpdateRequest();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.UpdateAsync(id, It.IsAny<Expression<Func<SetPropertyCalls<AccountModel>, SetPropertyCalls<AccountModel>>>>(), default))
			.Returns(Task.FromResult(1));
		AccountService sut = CreateMockedInstance(accountMock.Object);

		ErrorOr<Updated> result = await sut.Update(id, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
