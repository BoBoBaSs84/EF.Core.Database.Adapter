﻿using System.Linq.Expressions;

using BB84.Home.Application.Contracts.Responses.Finance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Services.Finance;
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
	[TestCategory(nameof(AccountService.GetByUserId))]
	public async Task GetByUserIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		AccountService sut = CreateMockedInstance();

		ErrorOr<IEnumerable<AccountResponse>> result = await sut.GetByUserId(id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AccountServiceErrors.GetByUserIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AccountService.GetByUserId))]
	public async Task GetByUserIdShouldReturnResponseWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		IEnumerable<AccountEntity> accounts = [new(), new(), new()];
		Mock<IAccountRepository> accountMock = new();
		accountMock.Setup(x => x.GetManyByConditionAsync(It.IsAny<Expression<Func<AccountEntity, bool>>>(), null, false, null, null, null, false, default))
			.Returns(Task.FromResult(accounts));
		AccountService sut = CreateMockedInstance(accountMock.Object);

		ErrorOr<IEnumerable<AccountResponse>> result = await sut.GetByUserId(id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Count().Should().Be(accounts.Count());
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
