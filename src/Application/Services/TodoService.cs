using Application.Contracts.Requests.Todo;
using Application.Contracts.Responses.Todo;
using Application.Errors.Services;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using Domain.Errors;
using Domain.Models.Todo;
using Domain.Results;

using Microsoft.Extensions.Logging;

namespace Application.Services;

/// <summary>
/// The todo service implementation.
/// </summary>
/// <param name="loggerService">The logger service instance to use.</param>
/// <param name="repositoryService">The repository service instance to use.</param>
/// <param name="mapper">The auto mapper instance to use.</param>
internal sealed class TodoService(ILoggerService<TodoService> loggerService, IRepositoryService repositoryService, IMapper mapper) : ITodoService
{
	private readonly ILoggerService<TodoService> _loggerService = loggerService;
	private readonly IRepositoryService _repositoryService = repositoryService;
	private readonly IMapper _mapper = mapper;

	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public async Task<ErrorOr<Created>> CreateListByUserId(Guid userId, ListCreateRequest request, CancellationToken token = default)
	{
		try
		{
			List list = MapFromCreateListRequest(request);

			ListUser user = new() { TodoList = list, UserId = userId };

			list.Users.Add(user);

			await _repositoryService.TodoListRepository.CreateAsync(list, token)
				.ConfigureAwait(false);

			_ = await _repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, request, ex);
			return TodoServiceErrors.CreateListByUserFailed(userId);
		}
	}

	public async Task<ErrorOr<Created>> CreateItemByListId(Guid listId, ItemCreateRequest request, CancellationToken token = default)
	{
		try
		{
			List? list = await _repositoryService.TodoListRepository
				.GetByConditionAsync(expression: x => x.Id.Equals(listId), cancellationToken: token)
				.ConfigureAwait(false);

			if (list is null)
				return TodoServiceErrors.GetListByListIdNotFound(listId);

			Item item = MapFromCreateItemRequest(request);

			item.ListId = listId;

			await _repositoryService.TodoItemRepository.CreateAsync(item, token)
				.ConfigureAwait(false);

			_ = await _repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, request, ex);
			return TodoServiceErrors.CreateItemByListIdFailed(listId);
		}
	}

	public async Task<ErrorOr<ListResponse>> GetListByListId(Guid userId, Guid listId, CancellationToken token = default)
	{
		try
		{
			List? todoList = await _repositoryService.TodoListRepository
				.GetByConditionAsync(
					expression: x => x.Users.Select(x => x.UserId).Contains(userId) && x.Id.Equals(listId),
					includeProperties: [nameof(List.Items)],
					cancellationToken: token
					)
				.ConfigureAwait(false);

			if (todoList is null)
				return TodoServiceErrors.GetListByListIdNotFound(userId);

			ListResponse response = MapToListResponse(todoList);

			return response;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{nameof(userId)}:{userId}", $"{nameof(listId)}:{listId}"];
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return TodoServiceErrors.GetListByIdFailed(listId);
		}
	}

	public async Task<ErrorOr<IEnumerable<ListResponse>>> GetListsByUserId(Guid userId, CancellationToken token = default)
	{
		try
		{
			IEnumerable<List> todoLists = await _repositoryService.TodoListRepository
				.GetManyByConditionAsync(expression: x => x.Users.Select(x => x.UserId).Contains(userId), cancellationToken: token)
				.ConfigureAwait(false);

			IEnumerable<ListResponse> response = todoLists.Select(MapToListResponse);

			return response.ToList();
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{nameof(userId)}:{userId}"];
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return TodoServiceErrors.GetListsByUserIdFailed(userId);
		}
	}

	private List MapFromCreateListRequest(ListCreateRequest request)
		=> _mapper.Map<List>(request);

	private Item MapFromCreateItemRequest(ItemCreateRequest request)
		=> _mapper.Map<Item>(request);

	private ListResponse MapToListResponse(List todoList)
		=> _mapper.Map<ListResponse>(todoList);
}
