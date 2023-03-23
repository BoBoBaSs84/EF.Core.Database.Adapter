﻿using Application.Errors.Base;
using Application.Features.Requests;
using Application.Services;
using Domain.Extensions;
using System.Globalization;
using RESX = Application.Properties.ServiceErrors;

namespace Application.Errors.Services;

/// <summary>
/// The static calendar day service errors class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the calendar day service.
/// </remarks>
public static class CalendarDayServiceErrors
{
	private static readonly CultureInfo CurrentCulture = Domain.Statics.CurrentCulture;
	private const string ErrorPrefix = $"{nameof(CalendarDayServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarDayService.GetByDate(DateTime, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetByDateFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByDateFailed)}",
			RESX.CalendarDayService_GetByDate_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarDayService.GetByDate(DateTime, bool, CancellationToken)"/> method.
	/// </summary>
	public static ApiError GetByDateNotFound(DateTime date) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByDateNotFound)}",
			RESX.CalendarDayService_GetByDate_NotFound.Format(CurrentCulture, date));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarDayService.GetById(int, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetByIdFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByIdFailed)}",
			RESX.CalendarDayService_GetById_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarDayService.GetById(int, bool, CancellationToken)"/> method.
	/// </summary>
	public static ApiError GetByIdNotFound(int id) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByIdNotFound)}",
			RESX.CardTypeService_GetByName_NotFound.Format(CurrentCulture, id));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarDayService.GetPagedByParameters(CalendarDayParameters, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetPagedByParametersFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetPagedByParametersFailed)}",
			RESX.CalendarDayService_GetPagedByParameters_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CalendarDayService.GetPagedByParameters(CalendarDayParameters, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetPagedByParametersNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetPagedByParametersNotFound)}",
			RESX.CalendarDayService_GetPagedByParameters_NotFound);
}