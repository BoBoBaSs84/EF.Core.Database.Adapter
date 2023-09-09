using Application.Contracts.Requests.Attendance;
using Application.Errors.Base;
using Application.Interfaces.Application;

using RESX = Application.Properties.ServiceErrors;

namespace Application.Errors.Services;

/// <summary>
/// The static <see cref="AttendanceSettingsServiceErrors"/> class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the attendance settings service.
/// </remarks>
public static class AttendanceSettingsServiceErrors
{
	private const string ErrorPrefix = $"{nameof(AttendanceSettingsServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="IAttendanceSettingsService.Create(Guid, AttendanceSettingsRequest, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError CreateConflict =
		ApiError.CreateConflict($"{ErrorPrefix}.{nameof(CreateConflict)}",
			RESX.AccountSettingsService_Create_Conflict);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="IAttendanceSettingsService.Create(Guid, AttendanceSettingsRequest, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError CreateFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateFailed)}",
			RESX.AccountSettingsService_Create_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="IAttendanceSettingsService.Get(Guid, bool, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError GetFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetFailed)}",
			RESX.AccountSettingsService_Get_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="IAttendanceSettingsService.Get(Guid, bool, CancellationToken)"/> or
	/// <see cref="IAttendanceSettingsService.Update(Guid, AttendanceSettingsRequest, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError GetNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetNotFound)}",
			RESX.AccountSettingsService_Get_NotFound);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="IAttendanceSettingsService.Update(Guid, AttendanceSettingsRequest, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError UpdateFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetNotFound)}",
			RESX.AccountSettingsService_Update_Failed);
}
