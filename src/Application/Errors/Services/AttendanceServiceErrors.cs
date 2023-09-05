using Application.Contracts.Requests.Attendance;
using Application.Errors.Base;
using Application.Features.Requests;
using Application.Services;

using Domain.Extensions;

using RESX = Application.Properties.ServiceErrors;

namespace Application.Errors.Services;

/// <summary>
/// The static <see cref="AttendanceServiceErrors"/> class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the attendance service.
/// </remarks>
public static class AttendanceServiceErrors
{
	private const string ErrorPrefix = $"{nameof(AttendanceServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AttendanceService.Create(Guid, AttendanceCreateRequest, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError CreateFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateFailed)}",
			RESX.AttendanceService_Create_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AttendanceService.Create(Guid, IEnumerable{AttendanceCreateRequest}, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError CreateManyFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateManyFailed)}",
			RESX.AttendanceService_CreateMany_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AttendanceService.Delete(Guid, Guid, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError DeleteFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(DeleteFailed)}",
			RESX.AttendanceService_Delete_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AttendanceService.Delete(Guid, Guid, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError DeleteNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(DeleteNotFound)}",
			RESX.AttendanceService_Delete_NotFound);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AttendanceService.Delete(Guid, IEnumerable{Guid}, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError DeleteManyFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(DeleteManyFailed)}",
			RESX.AttendanceService_DeleteMany_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AttendanceService.Delete(Guid, IEnumerable{Guid}, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError DeleteManyNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(DeleteManyNotFound)}",
			RESX.AttendanceService_DeleteMany_NotFound);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AttendanceService.Get(Guid, DateTime, bool, CancellationToken)"/>
	/// method.
	/// </summary>
	/// <param name="date">The date parameter.</param>
	public static ApiError GetByDateFailed(DateTime date) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{GetByDateFailed}",
			RESX.AttendanceService_GetByDate_Failed.FormatInvariant(date));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AttendanceService.Get(Guid, DateTime, bool, CancellationToken)"/>
	/// method.
	/// </summary>
	/// <param name="date">The date parameter.</param>
	public static ApiError GetByDateNotFound(DateTime date) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetByDateNotFound}",
			RESX.AttendanceService_GetByDate_NotFound.FormatInvariant(date));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AttendanceService.Get(Guid, Guid, bool, CancellationToken)"/>
	/// method.
	/// </summary>
	/// <param name="calendarDayId">The calendar day identifier parameter.</param>
	public static ApiError GetByIdFailed(Guid calendarDayId) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{GetByIdFailed}",
			RESX.AttendanceService_GetById_Failed.FormatInvariant(calendarDayId));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AttendanceService.Get(Guid, DateTime, bool, CancellationToken)"/>
	/// method.
	/// </summary>
	/// <param name="calendarDayId">The calendar day identifier parameter.</param>
	public static ApiError GetByIdNotFound(Guid calendarDayId) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetByIdNotFound}",
			RESX.AttendanceService_GetById_NotFound.FormatInvariant(calendarDayId));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AttendanceService.Get(Guid, AttendanceParameters, bool, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError GetPagedByParametersFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetPagedByParametersFailed)}",
			RESX.AttendanceService_GetPagedByParameters_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AttendanceService.Get(Guid, AttendanceParameters, bool, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError GetPagedByParametersNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetPagedByParametersNotFound)}",
			RESX.AttendanceService_GetPagedByParameters_NotFound);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AttendanceService.Update(AttendanceUpdateRequest, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError UpdateFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(UpdateFailed)}",
			RESX.AttendanceService_Update_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AttendanceService.Update(AttendanceUpdateRequest, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError UpdateNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(UpdateNotFound)}",
			RESX.AttendanceService_Update_NotFound);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AttendanceService.Update(IEnumerable{AttendanceUpdateRequest}, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError UpdateManyFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(UpdateManyFailed)}",
			RESX.AttendanceService_UpdateMany_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AttendanceService.Update(IEnumerable{AttendanceUpdateRequest}, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError UpdateManyNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(UpdateManyNotFound)}",
			RESX.AttendanceService_UpdateMany_NotFound);
}
