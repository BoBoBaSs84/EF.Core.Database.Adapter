using System.Drawing;

using Application.Contracts.Requests.Todo;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;
using Application.Interfaces.Infrastructure.Services;
using Application.Services.Todo;

using AutoMapper;

using BB84.Extensions;

using Domain.Enumerators.Todo;

using Moq;

namespace ApplicationTests.Services.Todo;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class TodoServiceTests : ApplicationTestBase
{
	private const string UnitTest = "UnitTest";

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

	private static ListCreateRequest GetListCreateRequest()
	{
		ListCreateRequest request = new()
		{
			Title = UnitTest,
			Color = Color.White.ToRGBHexString()
		};

		return request;
	}

	private static ListUpdateRequest GetListUpdateRequest()
	{
		ListUpdateRequest request = new()
		{
			Title = UnitTest,
			Color = Color.Black.ToRGBHexString()
		};

		return request;
	}

	private static ItemCreateRequest GetItemCreateRequest()
	{
		ItemCreateRequest request = new()
		{
			Title = UnitTest,
			Priority = PriorityLevelType.NONE,
			Reminder = DateTime.Today,
			Note = UnitTest
		};

		return request;
	}

	private static ItemUpdateRequest GetItemUpdateRequest()
	{
		ItemUpdateRequest request = new()
		{
			Title = UnitTest,
			Priority = PriorityLevelType.NONE,
			Reminder = DateTime.Today,
			Note = UnitTest,
			Done = true
		};

		return request;
	}
}