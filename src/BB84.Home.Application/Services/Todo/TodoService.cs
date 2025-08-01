using AutoMapper;

using BB84.Home.Application.Contracts.Requests.Todo;
using BB84.Home.Application.Contracts.Responses.Todo;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Application.Services.Todo;
using BB84.Home.Application.Interfaces.Infrastructure.Services;
using BB84.Home.Application.Interfaces.Presentation.Services;
using BB84.Home.Domain.Entities.Todo;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

using Microsoft.Extensions.Logging;

namespace BB84.Home.Application.Services.Todo;

/// <summary>
/// Provides functionality for managing todo list and item records, including creation,
/// retrieval, updating, and deletion.
/// </summary>
/// <param name="loggerService">The logger service for logging errors and information.</param>
/// <param name="userService"> The service providing information about the current user.</param>
/// <param name="repositoryService">The repository service for accessing data repositories.</param>
/// <param name="mapper">The mapper for converting between domain entities and data transfer objects.</param>
internal sealed class TodoService(ILoggerService<TodoService> loggerService, ICurrentUserService userService, IRepositoryService repositoryService, IMapper mapper) : ITodoService
{
	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public async Task<ErrorOr<Created>> CreateListAsync(ListCreateRequest request, CancellationToken token = default)
	{
		try
		{
			ListEntity list = MapFromRequest(request);
			list.UserId = userService.UserId;

			await repositoryService.TodoListRepository.CreateAsync(list, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, request, ex);
			return TodoServiceErrors.CreateListByUserFailed(userService.UserId);
		}
	}

	public async Task<ErrorOr<Created>> CreateItemAsync(Guid listId, ItemCreateRequest request, CancellationToken token = default)
	{
		try
		{
			ListEntity? list = await repositoryService.TodoListRepository
				.GetByIdAsync(listId, token: token)
				.ConfigureAwait(false);

			if (list is null)
				return TodoServiceErrors.GetListByIdNotFound(listId);

			ItemEntity item = MapFromRequest(request);
			item.ListId = listId;

			await repositoryService.TodoItemRepository.CreateAsync(item, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, request, ex);
			return TodoServiceErrors.CreateItemByListIdFailed(listId);
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteListAsync(Guid listId, CancellationToken token = default)
	{
		try
		{
			ListEntity? listEntity = await repositoryService.TodoListRepository
				.GetByIdAsync(listId, token: token)
				.ConfigureAwait(false);

			if (listEntity is null)
				return TodoServiceErrors.GetListByIdNotFound(listId);

			await repositoryService.TodoListRepository
				.DeleteAsync(listEntity, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, listId, ex);
			return TodoServiceErrors.DeleteListByIdFailed(listId);
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteItemAsync(Guid itemId, CancellationToken token = default)
	{
		try
		{
			ItemEntity? itemEntity = await repositoryService.TodoItemRepository
				.GetByIdAsync(itemId, token: token)
				.ConfigureAwait(false);

			if (itemEntity is null)
				return TodoServiceErrors.GetItemByIdNotFound(itemId);

			await repositoryService.TodoItemRepository
				.DeleteAsync(itemEntity, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, itemId, ex);
			return TodoServiceErrors.DeleteItemByIdFailed(itemId);
		}
	}

	public async Task<ErrorOr<ListResponse>> GetListAsync(Guid listId, CancellationToken token = default)
	{
		try
		{
			ListEntity? todoList = await repositoryService.TodoListRepository
				.GetByIdAsync(listId, token: token, includeProperties: nameof(ListEntity.Items))
				.ConfigureAwait(false);

			if (todoList is null)
				return TodoServiceErrors.GetListByIdNotFound(listId);

			ListResponse response = MapToResponse(todoList);

			return response;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, listId, ex);
			return TodoServiceErrors.GetListByIdFailed(listId);
		}
	}

	public async Task<ErrorOr<IEnumerable<ListResponse>>> GetAllListsAsync(CancellationToken token = default)
	{
		try
		{
			IEnumerable<ListEntity> todoLists = await repositoryService.TodoListRepository
				.GetAllAsync(token: token)
				.ConfigureAwait(false);

			IEnumerable<ListResponse> response = MapToResponse(todoLists);

			return response.ToList();
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, userService.UserId, ex);
			return TodoServiceErrors.GetListsByUserIdFailed(userService.UserId);
		}
	}

	public async Task<ErrorOr<Updated>> UpdateListAsync(Guid listId, ListUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			ListEntity? list = await repositoryService.TodoListRepository
				.GetByIdAsync(listId, trackChanges: true, token: token)
				.ConfigureAwait(false);

			if (list is null)
				return TodoServiceErrors.GetListByIdNotFound(listId);

			_ = MapFromRequest(request, list);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, listId, ex);
			return TodoServiceErrors.UpdateListByIdFailed(listId);
		}
	}

	public async Task<ErrorOr<Updated>> UpdateItemAsync(Guid itemId, ItemUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			ItemEntity? item = await repositoryService.TodoItemRepository
				.GetByIdAsync(itemId, trackChanges: true, token: token)
				.ConfigureAwait(false);

			if (item is null)
				return TodoServiceErrors.GetItemByIdNotFound(itemId);

			_ = MapFromRequest(request, item);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, itemId, ex);
			return TodoServiceErrors.UpdateItemByIdFailed(itemId);
		}
	}

	private ListEntity MapFromRequest(ListCreateRequest request)
		=> mapper.Map<ListEntity>(request);

	private ItemEntity MapFromRequest(ItemCreateRequest request)
		=> mapper.Map<ItemEntity>(request);

	private ListResponse MapToResponse(ListEntity list)
		=> mapper.Map<ListResponse>(list);

	private IEnumerable<ListResponse> MapToResponse(IEnumerable<ListEntity> lists)
		=> lists.Select(MapToResponse);

	private ListEntity MapFromRequest(ListUpdateRequest request, ListEntity list)
		=> mapper.Map(request, list);

	private ItemEntity MapFromRequest(ItemUpdateRequest request, ItemEntity item)
		=> mapper.Map(request, item);
}
