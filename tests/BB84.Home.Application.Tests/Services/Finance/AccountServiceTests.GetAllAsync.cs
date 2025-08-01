﻿using BB84.Home.Application.Contracts.Responses.Finance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Tests;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Finance;
using BB84.Home.Domain.Errors;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Finance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AccountServiceTests : ApplicationTestBase
{
	[TestMethod]
	public async Task GetAllAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		_currentUserServiceMock.Setup(x => x.UserId)
			.Returns(userId);

		ErrorOr<IEnumerable<AccountResponse>> result = await _sut
			.GetAllAsync(token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.GetByUserIdFailed(userId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), userId, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task GetAllAsyncShouldReturnResponseWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		IEnumerable<AccountEntity> accounts = [new(), new(), new()];
		Mock<IAccountRepository> accountRepoMock = new();
		accountRepoMock.Setup(x => x.GetAllAsync(false, false, token))
			.Returns(Task.FromResult(accounts));
		_repositoryServiceMock.Setup(x => x.AccountRepository)
			.Returns(accountRepoMock.Object);

		ErrorOr<IEnumerable<AccountResponse>> result = await _sut
			.GetAllAsync(token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Count().Should().Be(accounts.Count());
			accountRepoMock.Verify(x => x.GetAllAsync(false, false, token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), userId, It.IsAny<Exception>()), Times.Never);
		});
	}
}
