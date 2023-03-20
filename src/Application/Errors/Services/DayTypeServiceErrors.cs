using Application.Errors.Base;
using Application.Features.Requests;
using Application.Services;
using Domain.Extensions;
using RESX = Application.Properties.Resources;

namespace Application.Errors.Services;

/// <summary>
/// The static day type service errors class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the day type service.
/// </remarks>
internal static class DayTypeServiceErrors
{
	private const string ErrorPrefix = $"{nameof(DayTypeServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="DayTypeService.GetById(int, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetByIdFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByIdFailed)}",
			RESX.DayTypeServiceErrors_GetByIdFailed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="DayTypeService.GetById(int, bool, CancellationToken)"/> method.
	/// </summary>
	public static ApiError GetByIdNotFound(int id) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByIdNotFound)}",
			RESX.DayTypeServiceErrors_GetByIdNotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="DayTypeService.GetByName(string, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetByNameFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByNameFailed)}",
			RESX.DayTypeServiceErrors_GetByNameFailed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="DayTypeService.GetByName(string, bool, CancellationToken)"/> method.
	/// </summary>
	public static ApiError GetByNameNotFound(string name) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByNameNotFound)}",
			RESX.DayTypeServiceErrors_GetByNameNotFound.FormatInvariant(name));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="DayTypeService.GetPagedByParameters(DayTypeParameters, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetPagedByParametersFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetPagedByParametersFailed)}",
			RESX.DayTypeServiceErrors_GetPagedByParametersFailed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="DayTypeService.GetPagedByParameters(DayTypeParameters, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetPagedByParametersNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetPagedByParametersNotFound)}",
			RESX.DayTypeServiceErrors_GetPagedByParametersNotFound);
}
