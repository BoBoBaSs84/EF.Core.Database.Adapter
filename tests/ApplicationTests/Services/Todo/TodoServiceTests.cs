using Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;
using Application.Interfaces.Infrastructure.Services;
using Application.Services.Todo;

using AutoMapper;

using Moq;

namespace ApplicationTests.Services.Todo;

[TestClass]
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