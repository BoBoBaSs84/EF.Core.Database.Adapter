using Application.Errors.Base;
using Application.Features.Requests;
using Application.Services;
using Domain.Extensions;
using RESX = Application.Properties.Resources;

namespace Application.Errors.Services;

/// <summary>
/// The static calendar day service errors class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the calendar day service.
/// </remarks>
public static class CalendarDayServiceErrors
{
	private const string ErrorPrefix = $"{nameof(CalendarDayServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarDayService.GetByDate(DateTime, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetByDateFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByDateFailed)}",
			RESX.CalendarDayServiceErrors_GetByDateFailed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarDayService.GetByDate(DateTime, bool, CancellationToken)"/> method.
	/// </summary>
	public static ApiError GetByDateNotFound(DateTime date) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByDateNotFound)}",
			RESX.CalendarDayServiceErrors_GetByDateNotFound.FormatInvariant(date));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarDayService.GetById(int, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetByIdFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByIdFailed)}",
			RESX.CalendarDayServiceErrors_GetByIdFailed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarDayService.GetById(int, bool, CancellationToken)"/> method.
	/// </summary>
	public static ApiError GetByIdNotFound(int id) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByIdNotFound)}",
			RESX.CalendarDayServiceErrors_GetByIdNotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarDayService.GetPagedByParameters(CalendarDayParameters, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetPagedByParametersFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetPagedByParametersFailed)}",
			RESX.CalendarDayServiceErrors_GetPagedByParametersFailed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarDayService.GetPagedByParameters(CalendarDayParameters, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetPagedByParametersNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetPagedByParametersNotFound)}",
			RESX.CalendarDayServiceErrors_GetPagedByParametersNotFound);
}
