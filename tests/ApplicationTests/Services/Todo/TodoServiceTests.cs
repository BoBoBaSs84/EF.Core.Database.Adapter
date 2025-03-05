using AutoMapper;

using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;
using BB84.Home.Application.Interfaces.Infrastructure.Services;
using BB84.Home.Application.Services.Todo;
using BB84.Home.Application.Tests;

using Moq;

namespace ApplicationTests.Services.Todo;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class TodoServiceTests : ApplicationTestBase
{
	private readonly IMapper _mapper = GetService<IMapper>();
	private Mock<ILoggerService<TodoService>> _loggerServiceMock = default!;
	private Mock<IRepositoryService> _repositoryServiceMock = default!;

	private TodoService CreateMockedInstance(IListRepository? listRepository = null, IItemRepository? itemRepository = null)
	{
		_loggerServiceMock = new();
		_repositoryServiceMock = new();

		if (listRepository is not null)
			_repositoryServiceMock.Setup(x => x.TodoListRepository).Returns(listRepository);

		if (itemRepository is not null)
			_repositoryServiceMock.Setup(x => x.TodoItemRepository).Returns(itemRepository);

		return new(_loggerServiceMock.Object, _repositoryServiceMock.Object, _mapper);
	}
}