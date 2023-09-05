using Application.Errors.Base;
using Application.Services;

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
	/// Error that indicates an exception during the <see cref="EnumeratorService.GetCardTypes"/> method.
	/// </summary>
	public static readonly ApiError GetCardTypesFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{GetCardTypesFailed}", RESX.EnumeratorService_GetCardTypes_Failed);

	/// <summary>
	/// Error that indicates an exception during the <see cref="EnumeratorService.GetDayTypes"/> method.
	/// </summary>
	public static readonly ApiError GetDayTypesFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{GetDayTypesFailed}", RESX.EnumeratorService_GetDayTypes_Failed);

	/// <summary>
	/// Error that indicates an exception during the <see cref="EnumeratorService.GetRoleTypes"/> method.
	/// </summary>
	public static readonly ApiError GetRoleTypesFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{GetRoleTypesFailed}", RESX.EnumeratorService_GetRoleTypes_Failed);
}
