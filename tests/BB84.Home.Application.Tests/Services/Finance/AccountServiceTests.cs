using AutoMapper;

using BB84.Home.Application.Interfaces.Infrastructure.Services;
using BB84.Home.Application.Interfaces.Presentation.Services;
using BB84.Home.Application.Services.Finance;
using BB84.Home.Application.Tests;

using Moq;

namespace ApplicationTests.Services.Finance;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AccountServiceTests : ApplicationTestBase
{
	private readonly AccountService _sut;
	private readonly Mock<ILoggerService<AccountService>> _loggerServiceMock = new();
	private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
	private readonly Mock<IRepositoryService> _repositoryServiceMock = new();
	private readonly IMapper _mapper = GetService<IMapper>();

	public AccountServiceTests()
		=> _sut = new(_loggerServiceMock.Object, _currentUserServiceMock.Object, _repositoryServiceMock.Object, _mapper);
}