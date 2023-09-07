using System.Globalization;

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
	private static readonly CultureInfo CurrentCulture = CultureInfo.CurrentCulture;
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
}
