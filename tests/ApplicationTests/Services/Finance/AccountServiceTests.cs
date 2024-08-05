﻿using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Interfaces.Infrastructure.Services;
using Application.Services.Finance;

using AutoMapper;

using Moq;

namespace ApplicationTests.Services.Finance;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AccountServiceTests : ApplicationTestBase
{
	private readonly IMapper _mapper = GetService<IMapper>();
	private Mock<ILoggerService<AccountService>> _loggerServiceMock = default!;
	private Mock<IRepositoryService> _repositoryServiceMock = default!;

	private AccountService CreateMockedInstance(IAccountRepository? accountRepository = null, ICardRepository? cardRepository = null)
	{
		_loggerServiceMock = new();
		_repositoryServiceMock = new();

		if (accountRepository is not null)
			_repositoryServiceMock.Setup(x => x.AccountRepository).Returns(accountRepository);

		if (cardRepository is not null)
			_repositoryServiceMock.Setup(x => x.CardRepository).Returns(cardRepository);

		return new(_loggerServiceMock.Object, _repositoryServiceMock.Object, _mapper);
	}
}