using AutoMapper;

using BB84.Home.Application.Interfaces.Infrastructure.Services;
using BB84.Home.Application.Interfaces.Presentation.Services;
using BB84.Home.Application.Services.Todo;
using BB84.Home.Application.Tests;

using Moq;

namespace ApplicationTests.Services.Todo;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class TodoServiceTests : ApplicationTestBase
{
	private readonly TodoService _sut;
	private readonly IMapper _mapper = GetService<IMapper>();
	private readonly Mock<ILoggerService<TodoService>> _loggerServiceMock = new();
	private readonly Mock<IRepositoryService> _repositoryServiceMock = new();
	private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();

	public TodoServiceTests()
		=> _sut = new(_loggerServiceMock.Object, _currentUserServiceMock.Object, _repositoryServiceMock.Object, _mapper);
}