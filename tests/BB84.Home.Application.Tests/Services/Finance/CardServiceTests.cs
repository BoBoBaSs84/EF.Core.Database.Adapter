using AutoMapper;

using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Interfaces.Infrastructure.Services;
using BB84.Home.Application.Interfaces.Presentation.Services;
using BB84.Home.Application.Services.Finance;
using BB84.Home.Application.Tests;

using Moq;

namespace ApplicationTests.Services.Finance;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class CardServiceTests : ApplicationTestBase
{
	private readonly CardService _sut;
	private readonly Mock<ILoggerService<CardService>> _loggerServiceMock = new();
	private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
	private readonly Mock<IRepositoryService> _repositoryServiceMock = new();
	private readonly IMapper _mapper = GetService<IMapper>();

	public CardServiceTests()
		=> _sut = new(_loggerServiceMock.Object, _currentUserServiceMock.Object, _repositoryServiceMock.Object, _mapper);

	private CardService CreateMockedInstance(IAccountRepository? accountRepository = null, ICardRepository? cardRepository = null)
	{
		if (accountRepository is not null)
			_repositoryServiceMock.Setup(x => x.AccountRepository).Returns(accountRepository);

		if (cardRepository is not null)
			_repositoryServiceMock.Setup(x => x.CardRepository).Returns(cardRepository);

		return new(_loggerServiceMock.Object, _currentUserServiceMock.Object, _repositoryServiceMock.Object, _mapper);
	}
}