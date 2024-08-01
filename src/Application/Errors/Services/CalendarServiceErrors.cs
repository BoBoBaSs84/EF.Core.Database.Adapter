using Application.Errors.Base;

using RESX = Application.Properties.ServiceErrors;

namespace Application.Errors.Services;

/// <summary>
/// The static calendar service errors class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the calendar service.
/// </remarks>
public static class CalendarServiceErrors
{
	private const string ErrorPrefix = $"{nameof(CalendarServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the calendar service.
	/// </summary>
	public static readonly ApiError GetByDateFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByDateFailed)}",
			RESX.CalendarDayService_GetByDate_Failed);

	/// <summary>
	/// Error that indicates an exception during the calendar service.
	/// </summary>
	public static readonly ApiError GetByIdFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByIdFailed)}",
			RESX.CalendarDayService_GetById_Failed);

	/// <summary>
	/// Error that indicates an exception during the calendar service.
	/// </summary>
	public static readonly ApiError GetCurrentDateFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetCurrentDateFailed)}",
			RESX.CalendarDayService_GetCurrentDate_Failed);

	/// <summary>
	/// Error that indicates an exception during the calendar service.
	/// </summary>
	public static readonly ApiError GetCurrentDateNotFound =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetCurrentDateNotFound)}",
			RESX.CalendarDayService_GetCurrentDate_NotFound);

	/// <summary>
	/// Error that indicates an exception during the calendar service.
	/// </summary>
	public static readonly ApiError GetPagedByParametersFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetPagedByParametersFailed)}",
			RESX.CalendarDayService_GetPagedByParameters_Failed);

	/// <summary>
	/// Error that indicates an exception during the calendar service.
	/// </summary>
	public static readonly ApiError GetPagedByParametersNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetPagedByParametersNotFound)}",
			RESX.CalendarDayService_GetPagedByParameters_NotFound);
}
