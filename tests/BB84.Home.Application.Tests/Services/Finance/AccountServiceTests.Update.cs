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
		accountMock.Setup(x => x.GetByIdAsync(id, default, true, default))
			.Returns(Task.FromResult<AccountEntity?>(null));
		AccountService sut = CreateMockedInstance(accountMock.Object);

		ErrorOr<Updated> result = await sut.Update(id, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.UpdateAccountNotFound(id));
			accountMock.Verify(x => x.GetByIdAsync(id, default, true, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.Update))]
	public async Task UpdateShouldReturnUpdatedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		AccountEntity account = new();
		AccountUpdateRequest request = RequestHelper.GetAccountUpdateRequest();
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetByIdAsync(id, default, true, default))
			.Returns(Task.FromResult<AccountEntity?>(account));
		AccountService sut = CreateMockedInstance(accountMock.Object);
		_repositoryServiceMock.Setup(x => x.CommitChangesAsync(default))
			.Returns(Task.FromResult(1));

		ErrorOr<Updated> result = await sut.Update(id, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			account.Type.Should().Be(request.Type);
			account.Provider.Should().Be(request.Provider);
			accountMock.Verify(x => x.GetByIdAsync(id, default, true, default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
