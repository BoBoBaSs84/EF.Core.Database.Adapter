using Application.Errors.Base;
using Application.Services;
using Domain.Extensions;
using RESX = Application.Properties.ServiceErrors;

namespace Application.Errors.Services;

/// <summary>
/// The static day type service errors class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the day type service.
/// </remarks>
internal static class DayTypeServiceErrors
{
	private static readonly CultureInfo CurrentCulture = CultureInfo.CurrentCulture;
	private const string ErrorPrefix = $"{nameof(DayTypeServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="DayTypeService.GetById(int, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetByIdFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByIdFailed)}",
			RESX.DayTypeService_GetById_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="DayTypeService.GetById(int, bool, CancellationToken)"/> method.
	/// </summary>
	public static ApiError GetByIdNotFound(int id) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByIdNotFound)}",
			RESX.DayTypeService_GetById_NotFound.Format(CurrentCulture, id));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="DayTypeService.GetByName(string, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetByNameFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByNameFailed)}",
			RESX.DayTypeService_GetByName_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="DayTypeService.GetByName(string, bool, CancellationToken)"/> method.
	/// </summary>
	public static ApiError GetByNameNotFound(string name) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByNameNotFound)}",
			RESX.DayTypeService_GetByName_NotFound.Format(CurrentCulture, name));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="DayTypeService.GetAll(bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetAllFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetAllFailed)}",
			RESX.DayTypeService_GetAll_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="DayTypeService.GetAll(bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetAllNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetAllNotFound)}",
			RESX.DayTypeService_GetAll_NotFound);
}
