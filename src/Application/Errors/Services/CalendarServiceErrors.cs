﻿using Application.Errors.Base;
using Application.Features.Requests;
using Application.Services;

using Domain.Extensions;

using RESX = Application.Properties.ServiceErrors;

namespace Application.Errors.Services;

/// <summary>
/// The static calendar day service errors class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the calendar day service.
/// </remarks>
public static class CalendarServiceErrors
{
	private const string ErrorPrefix = $"{nameof(CalendarServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarService.Get(DateTime, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetByDateFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByDateFailed)}",
			RESX.CalendarDayService_GetByDate_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarService.Get(DateTime, bool, CancellationToken)"/> method.
	/// </summary>
	public static ApiError GetByDateNotFound(DateTime date) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByDateNotFound)}",
			RESX.CalendarDayService_GetByDate_NotFound.FormatInvariant(date));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarService.Get(Guid, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetByIdFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByIdFailed)}",
			RESX.CalendarDayService_GetById_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarService.Get(Guid, bool, CancellationToken)"/> method.
	/// </summary>
	public static ApiError GetByIdNotFound(Guid id) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByIdNotFound)}",
			RESX.CalendarDayService_GetById_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarService.Get(bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetCurrentDateFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetCurrentDateFailed)}",
			RESX.CalendarDayService_GetCurrentDate_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarService.Get(bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetCurrentDateNotFound =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetCurrentDateNotFound)}",
			RESX.CalendarDayService_GetCurrentDate_NotFound);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarService.Get(CalendarParameters, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetPagedByParametersFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetPagedByParametersFailed)}",
			RESX.CalendarDayService_GetPagedByParameters_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarService.Get(CalendarParameters, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetPagedByParametersNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetPagedByParametersNotFound)}",
			RESX.CalendarDayService_GetPagedByParameters_NotFound);
}