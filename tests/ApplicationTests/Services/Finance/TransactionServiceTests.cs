﻿using AutoMapper;

using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Interfaces.Infrastructure.Services;
using BB84.Home.Application.Services.Finance;
using BB84.Home.Application.Tests;

using Moq;

namespace ApplicationTests.Services.Finance;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class TransactionServiceTests : ApplicationTestBase
{
	private readonly IMapper _mapper = GetService<IMapper>();
	private Mock<ILoggerService<TransactionService>> _loggerServiceMock = default!;
	private Mock<IRepositoryService> _repositoryServiceMock = default!;

	private TransactionService CreateMockedInstance(IAccountRepository? accountRepository = null, ICardRepository? cardRepository = null, ITransactionRepository? transactionRepository = null)
	{
		_loggerServiceMock = new();
		_repositoryServiceMock = new();

		if (accountRepository is not null)
			_repositoryServiceMock.Setup(x => x.AccountRepository).Returns(accountRepository);

		if (cardRepository is not null)
			_repositoryServiceMock.Setup(x => x.CardRepository).Returns(cardRepository);

		if (transactionRepository is not null)
			_repositoryServiceMock.Setup(x => x.TransactionRepository).Returns(transactionRepository);

		return new(_loggerServiceMock.Object, _repositoryServiceMock.Object, _mapper);
	}
}
