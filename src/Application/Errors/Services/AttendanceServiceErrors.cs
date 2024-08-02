using Application.Errors.Base;

using BB84.Extensions;

using RESX = Application.Properties.ServiceErrors;

namespace Application.Errors.Services;

/// <summary>
/// The static attendance service errors class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the attendance service.
/// </remarks>
public static class AttendanceServiceErrors
{
	private const string ErrorPrefix = $"{nameof(AttendanceServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	/// <param name="date">The attendance date to use.</param>
	public static ApiError CreateBadRequest(DateTime date)
		=> ApiError.CreateBadRequest($"{ErrorPrefix}.{nameof(CreateBadRequest)}",
			RESX.AttendanceService_Create_BadRequest.FormatInvariant(date));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	/// <param name="date">The attendance date to use.</param>
	public static ApiError CreateConflict(DateTime date)
		=> ApiError.CreateConflict($"{ErrorPrefix}.{nameof(CreateConflict)}",
			RESX.AttendanceService_Create_Conflict.FormatInvariant(date));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	/// <param name="date">The attendance date to use.</param>
	public static ApiError CreateFailed(DateTime date)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateFailed)}",
			RESX.AttendanceService_Create_Failed.FormatInvariant(date));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	/// <param name="dates">The attendance dates to use.</param>
	public static ApiError CreateMultipleBadRequest(IEnumerable<DateTime> dates)
		=> ApiError.CreateBadRequest($"{ErrorPrefix}.{nameof(CreateMultipleBadRequest)}",
			RESX.AttendanceService_CreateMultiple_BadRequest.FormatInvariant(string.Join(',', dates)));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	/// <param name="dates">The attendance dates to use.</param>
	public static ApiError CreateMultipleConflict(IEnumerable<DateTime> dates)
		=> ApiError.CreateConflict($"{ErrorPrefix}.{nameof(CreateMultipleConflict)}",
			RESX.AttendanceService_CreateMultiple_Conflict.FormatInvariant(string.Join(',', dates)));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	/// <param name="dates">The attendance dates to use.</param>
	public static ApiError CreateMultipleFailed(IEnumerable<DateTime> dates)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateMultipleFailed)}",
			RESX.AttendanceService_CreateMultiple_Failed.FormatInvariant(string.Join(',', dates)));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	/// <param name="id">The attendance identifier to use.</param>
	public static ApiError DeleteByIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(DeleteByIdFailed)}",
			RESX.AttendanceService_DeleteById_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	/// <param name="ids">The attendance identifiers to use.</param>
	public static ApiError DeleteByIdsFailed(IEnumerable<Guid> ids)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(DeleteByIdsFailed)}",
			RESX.AttendanceService_DeleteById_Failed.FormatInvariant(string.Join(',', ids)));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	/// <param name="date">The attendance date to use.</param>
	public static ApiError GetByDateFailed(DateTime date)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByDateFailed)}",
			RESX.AttendanceService_GetByDate_Failed.FormatInvariant(date));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	/// <param name="date">The attendance date to use.</param>
	public static ApiError GetByDateNotFound(DateTime date)
		=> ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByDateNotFound)}",
			RESX.AttendanceService_GetByDate_NotFound.FormatInvariant(date));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	/// <param name="id">The attendance identifier to use.</param>
	public static ApiError GetByIdNotFound(Guid id)
		=> ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByIdNotFound)}",
			RESX.AttendanceService_GetById_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	/// <param name="ids">The attendance identifiers to use.</param>
	public static ApiError GetByIdsNotFound(IEnumerable<Guid> ids)
		=> ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByIdsNotFound)}",
			RESX.AttendanceService_GetByIds_NotFound.FormatInvariant(string.Join(',', ids)));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	public static readonly ApiError GetPagedListByParametersFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetPagedListByParametersFailed)}",
			RESX.AttendanceService_GetPagedByParameters_Failed);

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	/// <param name="id">The attendance identifier to use.</param>
	public static ApiError UpdateBadRequest(Guid id) =>
		ApiError.CreateBadRequest($"{ErrorPrefix}.{nameof(UpdateBadRequest)}",
			RESX.AttendanceService_Update_BadRequest.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	/// <param name="id">The attendance identifier to use.</param>
	public static ApiError UpdateFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(UpdateFailed)}",
			RESX.AttendanceService_Update_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	/// <param name="ids">The attendance identifiers to use.</param>
	public static ApiError UpdateMultipleBadRequest(IEnumerable<Guid> ids) =>
		ApiError.CreateBadRequest($"{ErrorPrefix}.{nameof(UpdateMultipleBadRequest)}",
			RESX.AttendanceService_Update_BadRequest.FormatInvariant(string.Join(',', ids)));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	/// <param name="ids">The attendance identifiers to use.</param>
	public static ApiError UpdateMultipleFailed(IEnumerable<Guid> ids)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(UpdateMultipleFailed)}",
			RESX.AttendanceService_Update_Failed.FormatInvariant(string.Join(',', ids)));
}
