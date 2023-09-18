using Application.Errors.Base;

using Domain.Extensions;

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
	/// <param name="date">The date of the calendar entry.</param>
	public static ApiError GetByDateNotFound(DateTime date) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByDateNotFound)}",
			RESX.CalendarDayService_GetByDate_NotFound.ToInvariant(date));

	/// <summary>
	/// Error that indicates an exception during the calendar service.
	/// </summary>
	public static readonly ApiError GetByIdFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByIdFailed)}",
			RESX.CalendarDayService_GetById_Failed);

	/// <summary>
	/// Error that indicates an exception during the calendar service.
	/// </summary>
	/// <param name="id">The identifier of the calendar entry.</param>
	public static ApiError GetByIdNotFound(Guid id) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByIdNotFound)}",
			RESX.CalendarDayService_GetById_NotFound.ToInvariant(id));

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
