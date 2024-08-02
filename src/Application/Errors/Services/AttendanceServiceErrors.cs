﻿using Application.Errors.Base;

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
	public static readonly ApiError CreateFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateFailed)}",
			RESX.AttendanceService_Create_Failed);

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	public static ApiError CreateBadRequest(DateTime date) =>
		ApiError.CreateBadRequest($"{ErrorPrefix}.{nameof(CreateBadRequest)}",
			RESX.AttendanceService_Create_BadRequest.FormatInvariant(date));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	/// <param name="date">The date that inflicts the conflict.</param>
	public static ApiError CreateConflict(DateTime date) =>
		ApiError.CreateConflict($"{ErrorPrefix}.{nameof(CreateConflict)}",
			RESX.AttendanceService_Create_Conflict.FormatInvariant(date.ToShortDateString()));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	/// <param name="date">The date that was not found.</param>
	public static ApiError CreateNotFound(DateTime date) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(CreateNotFound)}",
			RESX.AttendanceService_Create_NotFound.FormatInvariant(date.ToShortDateString()));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	public static readonly ApiError CreateManyFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateManyFailed)}",
			RESX.AttendanceService_CreateMany_Failed);

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
	public static readonly ApiError GetPagedByParametersFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetPagedByParametersFailed)}",
			RESX.AttendanceService_GetPagedByParameters_Failed);

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	public static readonly ApiError GetPagedByParametersNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetPagedByParametersNotFound)}",
			RESX.AttendanceService_GetPagedByParameters_NotFound);

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	public static ApiError UpdateBadRequest(Guid id) =>
		ApiError.CreateBadRequest($"{ErrorPrefix}.{nameof(UpdateBadRequest)}",
			RESX.AttendanceService_Update_BadRequest.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	public static readonly ApiError UpdateFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(UpdateFailed)}",
			RESX.AttendanceService_Update_Failed);

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	public static readonly ApiError UpdateNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(UpdateNotFound)}",
			RESX.AttendanceService_Update_NotFound);

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	public static readonly ApiError UpdateManyFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(UpdateManyFailed)}",
			RESX.AttendanceService_UpdateMany_Failed);

	/// <summary>
	/// Error that indicates an exception during the attendance service.
	/// </summary>
	public static readonly ApiError UpdateManyNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(UpdateManyNotFound)}",
			RESX.AttendanceService_UpdateMany_NotFound);
}
