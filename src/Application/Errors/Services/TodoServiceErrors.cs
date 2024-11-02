using Application.Errors.Base;

using BB84.Extensions;

using RESX = Application.Properties.ServiceErrors;

namespace Application.Errors.Services;

/// <summary>
/// The todo service errors class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the todo service.
/// </remarks>
public static class TodoServiceErrors
{
	private const string ErrorPrefix = nameof(TodoServiceErrors);

	/// <summary>
	/// Error that indicates an exception during the todo service.
	/// </summary>
	public static ApiError CreateListByUserFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateListByUserFailed)}",
			RESX.TodoService_CreateListByUserId_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the todo service.
	/// </summary>
	public static ApiError CreateItemByListIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateItemByListIdFailed)}",
			RESX.TodoService_CreateItemByListId_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the todo service.
	/// </summary>
	public static ApiError DeleteItemByIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(DeleteItemByIdFailed)}",
			RESX.TodoService_DeleteItemById_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the todo service.
	/// </summary>
	/// <param name="id">The list id that failed to create.</param>
	public static ApiError DeleteListByIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(DeleteListByIdFailed)}",
			RESX.TodoService_DeleteListById_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the todo service.
	/// </summary>
	public static ApiError GetListByIdNotFound(Guid id)
		=> ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetListByIdNotFound)}",
			RESX.TodoService_GetListById_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the todo service.
	/// </summary>
	public static ApiError GetListByIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetListByIdFailed)}",
			RESX.TodoService_GetListById_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the todo service.
	/// </summary>
	public static ApiError GetListsByUserIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetListsByUserIdFailed)}",
			RESX.TodoService_GetListsById_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the todo service.
	/// </summary>
	public static ApiError GetItemByIdNotFound(Guid id)
		=> ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetItemByIdNotFound)}",
			RESX.TodoService_GetItemById_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the todo service.
	/// </summary>
	public static ApiError UpdateItemByIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(UpdateItemByIdFailed)}",
			RESX.TodoService_UpdateItemById_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the todo service.
	/// </summary>
	public static ApiError UpdateListByIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(UpdateListByIdFailed)}",
			RESX.TodoService_UpdateListById_Failed.FormatInvariant(id));
}
