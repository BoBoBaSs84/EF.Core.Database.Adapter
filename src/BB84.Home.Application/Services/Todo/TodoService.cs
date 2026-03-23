using BB84.Home.Application.Contracts.Requests.Todo;
using BB84.Home.Application.Contracts.Responses.Todo;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Extensions;
using BB84.Home.Application.Interfaces.Application.Services.Todo;
using BB84.Home.Application.Interfaces.Infrastructure.Services;
using BB84.Home.Application.Interfaces.Presentation.Services;
using BB84.Home.Domain.Entities.Todo;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BB84.Home.Application.Services.Todo;

/// <summary>
/// Provides functionality for managing todo list and item records, including creation,
/// retrieval, updating, and deletion.
/// </summary>
/// <param name="loggerService">The logger service for logging errors and information.</param>
/// <param name="userService"> The service providing information about the current user.</param>
/// <param name="repositoryService">The repository service for accessing data repositories.</param>
internal sealed class TodoService(ILoggerService<TodoService> loggerService, ICurrentUserService userService, IRepositoryService repositoryService) : ITodoService
{
	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public async Task<ErrorOr<Created>> CreateListAsync(ListCreateRequest request, CancellationToken token = default)
	{
		try
		{
			ListEntity entity = request.ToEntity(userService.UserId);

			await repositoryService.TodoListRepository
				.CreateAsync(entity, token)
				.ConfigureAwait(false);

			_ = await repositoryService
				.CommitChangesAsync(token)
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

			ItemEntity item = request.ToEntity(listId);

			await repositoryService.TodoItemRepository
				.CreateAsync(item, token)
				.ConfigureAwait(false);

			_ = await repositoryService
				.CommitChangesAsync(token)
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
			ListResponse? listResponse = await repositoryService.TodoListRepository
				.GetByConditionAsync(
					expression: x => x.Id == listId,
					selector: listEntity => listEntity.ToResponse(),
					queryFilter: qf => qf.Include(x => x.Items),
					token: token)
				.ConfigureAwait(false);

			if (listResponse is null)
				return TodoServiceErrors.GetListByIdNotFound(listId);

			return listResponse;
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
			IReadOnlyList<ListEntity> entities = await repositoryService.TodoListRepository
				.GetAllAsync(token: token)
				.ConfigureAwait(false);

			IEnumerable<ListResponse> response = entities.Select(x => x.ToResponse());

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

			list = request.ToEntity(list);

			_ = await repositoryService
				.CommitChangesAsync(token)
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

			item = request.ToEntity(item);

			_ = await repositoryService
				.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, itemId, ex);
			return TodoServiceErrors.UpdateItemByIdFailed(itemId);
		}
	}
}
