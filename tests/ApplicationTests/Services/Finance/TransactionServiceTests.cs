using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Interfaces.Infrastructure.Services;
using Application.Services.Finance;

using AutoMapper;

using BB84.Extensions;

using Domain.Models.Finance;

using Moq;

using static BaseTests.Helpers.RandomHelper;
using static Domain.Constants.DomainConstants;

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

	private static TransactionModel CreateTransaction()
	{
		TransactionModel transaction = new()
		{
			Id = Guid.NewGuid(),
			CreatedBy = GetString(50),
			ModifiedBy = GetString(50),
			BookingDate = GetDateTime(),
			ValueDate = GetDateTime(),
			PostingText = GetString(100),
			ClientBeneficiary = GetString(250),
			Purpose = GetString(400),
			AccountNumber = GetString(RegexPatterns.IBAN).RemoveWhitespace(),
			BankCode = GetString(25),
			AmountEur = GetInt(-100, 250),
			CreditorId = GetString(25),
			MandateReference = GetString(50),
			CustomerReference = GetString(50)
		};
		return transaction;
	}
}
