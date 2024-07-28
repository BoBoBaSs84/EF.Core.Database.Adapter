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
	private const string ErrorPrefix = $"{nameof(TodoServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the todo service.
	/// </summary>
	/// <param name="id">The user id that failed to create.</param>
	public static ApiError CreateListByUserFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateListByUserFailed)}",
			$"{RESX.TodoService_CreateListByUserId_Failed.FormatInvariant(id)}");

	/// <summary>
	/// Error that indicates an exception during the todo service.
	/// </summary>
	/// <param name="id">The list id that failed to create.</param>
	public static ApiError CreateItemByListIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateItemByListIdFailed)}",
			$"{RESX.TodoService_CreateItemByListId_Failed.FormatInvariant(id)}");

	/// <summary>
	/// Error that indicates an exception during the todo service.
	/// </summary>
	/// <param name="id">The list id that was not found.</param>
	public static ApiError GetListByListIdNotFound(Guid id)
		=> ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetListByListIdNotFound)}",
			$"{RESX.TodoService_GetListById_NotFound.FormatInvariant(id)}");

	/// <summary>
	/// Error that indicates an exception during the todo service.
	/// </summary>
	/// <param name="id">The list id that failed to load.</param>
	public static ApiError GetListByIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetListByIdFailed)}",
			$"{RESX.TodoService_GetListById_Failed.FormatInvariant(id)}");

	/// <summary>
	/// Error that indicates an exception during the todo service.
	/// </summary>
	/// <param name="id">The user id that failed to load.</param>
	public static ApiError GetListsByUserIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetListsByUserIdFailed)}",
			$"{RESX.TodoService_GetListsById_Failed.FormatInvariant(id)}");
}
