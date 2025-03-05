using BB84.Extensions;
using BB84.Home.Application.Errors.Base;

using RESX = BB84.Home.Application.Properties.ServiceErrors;

namespace BB84.Home.Application.Errors.Services;

/// <summary>
/// The document service errors class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the document service.
/// </remarks>
public static class DocumentServiceErrors
{
	private const string ErrorPrefix = nameof(DocumentServiceErrors);

	/// <summary>
	/// Error that indicates an exception during the document service.
	/// </summary>
	public static ApiError CreateFailed(string document)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateFailed)}",
			RESX.DocumentService_Create_Failed.FormatInvariant(document));

	/// <summary>
	/// Error that indicates an exception during the document service.
	/// </summary>
	public static ApiError CreateMultipleFailed(IEnumerable<string> documents)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateMultipleFailed)}",
			RESX.DocumentService_CreateMultiple_Failed.FormatInvariant(string.Join(',', documents)));

	/// <summary>
	/// Error that indicates an exception during the document service.
	/// </summary>
	public static ApiError CreateMultipleBadRequest
		=> ApiError.CreateBadRequest($"{ErrorPrefix}.{nameof(CreateMultipleBadRequest)}",
			RESX.DocumentService_CreateMultiple_BadRequest);

	/// <summary>
	/// Error that indicates an exception during the document service.
	/// </summary>
	public static ApiError GetByIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByIdFailed)}",
			RESX.DocumentService_DeleteById_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the document service.
	/// </summary>
	public static ApiError GetByIdNotFound(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByIdNotFound)}",
			RESX.DocumentService_DeleteById_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the document service.
	/// </summary>
	public static ApiError GetPagedByParametersFailed
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetPagedByParametersFailed)}",
			RESX.DocumentService_GetPagedByParameters_Failed);

	/// <summary>
	/// Error that indicates an exception during the document service.
	/// </summary>
	public static ApiError DeleteByIdNotFound(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(DeleteByIdNotFound)}",
			RESX.DocumentService_DeleteById_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the document service.
	/// </summary>
	public static ApiError DeleteByIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(DeleteByIdFailed)}",
			RESX.DocumentService_DeleteById_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the document service.
	/// </summary>
	public static ApiError DeleteByIdsNotFound(IEnumerable<Guid> ids)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(DeleteByIdsNotFound)}",
			RESX.DocumentService_DeleteByIds_NotFound.FormatInvariant(string.Join(',', ids.Select(id => id))));

	/// <summary>
	/// Error that indicates an exception during the document service.
	/// </summary>
	public static ApiError DeleteByIdsFailed(IEnumerable<Guid> ids)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(DeleteByIdsFailed)}",
			RESX.DocumentService_DeleteByIds_Failed.FormatInvariant(string.Join(',', ids.Select(id => id))));

	/// <summary>
	/// Error that indicates an exception during the document service.
	/// </summary>
	public static ApiError UpdateByIdNotFound(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(UpdateByIdNotFound)}",
			RESX.DocumentService_UpdateById_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the document service.
	/// </summary>
	public static ApiError UpdateByIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(UpdateByIdFailed)}",
			RESX.DocumentService_UpdateById_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the document service.
	/// </summary>
	public static ApiError UpdateByIdsBadRequest
		=> ApiError.CreateBadRequest($"{ErrorPrefix}.{nameof(UpdateByIdsBadRequest)}",
			RESX.DocumentService_UpdateByIds_BadRequest);

	/// <summary>
	/// Error that indicates an exception during the document service.
	/// </summary>
	public static ApiError UpdateByIdsNotFound(IEnumerable<Guid> ids)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(UpdateByIdsNotFound)}",
			RESX.DocumentService_UpdateByIds_NotFound.FormatInvariant(string.Join(',', ids.Select(id => id))));

	/// <summary>
	/// Error that indicates an exception during the document service.
	/// </summary>
	public static ApiError UpdateByIdsFailed(IEnumerable<Guid> ids)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(UpdateByIdsFailed)}",
			RESX.DocumentService_UpdateByIds_Failed.FormatInvariant(string.Join(',', ids.Select(id => id))));
}
