using Application.Errors.Base;

using RESX = Application.Properties.ServiceErrors;

namespace Application.Errors.Services;

/// <summary>
/// The static enumerator service errors class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the enumerator service.
/// </remarks>
public static class EnumeratorServiceErrors
{
	private const string ErrorPrefix = $"{nameof(EnumeratorServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the enumerator service.
	/// </summary>
	public static readonly ApiError GetCardTypesFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetCardTypesFailed)}",
			RESX.EnumeratorService_GetCardTypes_Failed);

	/// <summary>
	/// Error that indicates an exception during the enumerator service.
	/// </summary>
	public static readonly ApiError GetAttendanceTypesFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetAttendanceTypesFailed)}",
			RESX.EnumeratorService_GetDayTypes_Failed);

	/// <summary>
	/// Error that indicates an exception during the enumerator service.
	/// </summary>
	public static readonly ApiError GetPriorityLevelTypesFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetPriorityLevelTypesFailed)}",
			RESX.EnumeratorService_GetPriorityLevelTypes_Failed);

	/// <summary>
	/// Error that indicates an exception during the enumerator service.
	/// </summary>
	public static readonly ApiError GetRoleTypesFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetRoleTypesFailed)}",
			RESX.EnumeratorService_GetRoleTypes_Failed);

	/// <summary>
	/// Error that indicates an exception during the enumerator service.
	/// </summary>
	public static readonly ApiError GetWorkDayTypesFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetWorkDayTypesFailed)}",
			RESX.EnumeratorService_GetWorkDayTypes_Failed);
}
