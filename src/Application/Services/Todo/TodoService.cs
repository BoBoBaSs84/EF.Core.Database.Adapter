using Application.Contracts.Requests.Todo;
using Application.Contracts.Responses.Todo;
using Application.Errors.Services;
using Application.Interfaces.Application.Todo;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using BB84.Extensions;

using Domain.Errors;
using Domain.Models.Todo;
using Domain.Results;

using Microsoft.Extensions.Logging;

namespace Application.Services.Todo;

/// <summary>
/// The todo service implementation.
/// </summary>
/// <param name="loggerService">The logger service instance to use.</param>
/// <param name="repositoryService">The repository service instance to use.</param>
/// <param name="mapper">The auto mapper instance to use.</param>
internal sealed class TodoService(ILoggerService<TodoService> loggerService, IRepositoryService repositoryService, IMapper mapper) : ITodoService
{
	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public async Task<ErrorOr<Created>> CreateListByUserId(Guid userId, ListCreateRequest request, CancellationToken token = default)
	{
		try
		{
			List list = MapFromCreateListRequest(request);

			list.Users = [new() { TodoList = list, UserId = userId }];

			await repositoryService.TodoListRepository.CreateAsync(list, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, request, ex);
			return TodoServiceErrors.CreateListByUserFailed(userId);
		}
	}

	public async Task<ErrorOr<Created>> CreateItemByListId(Guid listId, ItemCreateRequest request, CancellationToken token = default)
	{
		try
		{
			List? list = await repositoryService.TodoListRepository
				.GetByIdAsync(listId, token: token)
				.ConfigureAwait(false);

			if (list is null)
				return TodoServiceErrors.GetListByIdNotFound(listId);

			Item item = MapFromCreateItemRequest(request);

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

	public async Task<ErrorOr<Deleted>> DeleteListById(Guid listId, CancellationToken token = default)
	{
		try
		{
			int result = await repositoryService.TodoListRepository
				.DeleteAsync(listId, token)
				.ConfigureAwait(false);

			return result.Equals(0)
				? TodoServiceErrors.GetListByIdNotFound(listId)
				: Result.Deleted;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, listId, ex);
			return TodoServiceErrors.DeleteListByIdFailed(listId);
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteItemById(Guid itemId, CancellationToken token = default)
	{
		try
		{
			int result = await repositoryService.TodoItemRepository
				.DeleteAsync(itemId, token)
				.ConfigureAwait(false);

			return result.Equals(0)
				? TodoServiceErrors.GetItemByIdNotFound(itemId)
				: Result.Deleted;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, itemId, ex);
			return TodoServiceErrors.DeleteItemByIdFailed(itemId);
		}
	}

	public async Task<ErrorOr<ListResponse>> GetListById(Guid listId, CancellationToken token = default)
	{
		try
		{
			List? todoList = await repositoryService.TodoListRepository
				.GetByConditionAsync(expression: x => x.Id.Equals(listId), token: token, includeProperties: [nameof(List.Items)])
				.ConfigureAwait(false);

			if (todoList is null)
				return TodoServiceErrors.GetListByIdNotFound(listId);

			ListResponse response = MapToListResponse(todoList);

			return response;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, listId, ex);
			return TodoServiceErrors.GetListByIdFailed(listId);
		}
	}

	public async Task<ErrorOr<IEnumerable<ListResponse>>> GetListsByUserId(Guid userId, CancellationToken token = default)
	{
		try
		{
			IEnumerable<List> todoLists = await repositoryService.TodoListRepository
				.GetManyByConditionAsync(expression: x => x.Users.Select(x => x.UserId).Contains(userId), token: token)
				.ConfigureAwait(false);

			IEnumerable<ListResponse> response = todoLists.Select(MapToListResponse);

			return response.ToList();
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, userId, ex);
			return TodoServiceErrors.GetListsByUserIdFailed(userId);
		}
	}

	public async Task<ErrorOr<Updated>> UpdateListById(Guid listId, ListUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			List? list = await repositoryService.TodoListRepository
				.GetByIdAsync(listId, false, true, token)
				.ConfigureAwait(false);

			if (list is null)
				return TodoServiceErrors.GetListByIdNotFound(listId);

			list.Title = request.Title;
			list.Color = request.Color?.FromRGBHexString();

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

	public async Task<ErrorOr<Updated>> UpdateItemById(Guid itemId, ItemUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			Item? item = await repositoryService.TodoItemRepository
				.GetByIdAsync(itemId, false, true, token)
				.ConfigureAwait(false);

			if (item is null)
				return TodoServiceErrors.GetItemByIdNotFound(itemId);

			item.Title = request.Title;
			item.Note = request.Note;
			item.Priority = request.Priority;
			item.Reminder = request.Reminder;
			item.Done = request.Done;

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

	private List MapFromCreateListRequest(ListCreateRequest request)
		=> mapper.Map<List>(request);

	private Item MapFromCreateItemRequest(ItemCreateRequest request)
		=> mapper.Map<Item>(request);

	private ListResponse MapToListResponse(List todoList)
		=> mapper.Map<ListResponse>(todoList);
}
