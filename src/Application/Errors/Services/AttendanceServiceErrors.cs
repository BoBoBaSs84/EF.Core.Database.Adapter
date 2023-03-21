using Application.Errors.Base;
using Application.Features.Requests;
using Application.Services;
using RESX = Application.Properties.Resources;

namespace Application.Errors.Services;

/// <summary>
/// The static <see cref="AttendanceServiceErrors"/> class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the attendance service.
/// </remarks>
public static class AttendanceServiceErrors
{
	private const string ErrorPrefix = $"{nameof(AuthenticationServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AttendanceService.GetPagedByParameters(int, AttendanceParameters, bool, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError GetPagedByParametersFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetPagedByParametersFailed)}",
			RESX.AttendanceServiceErrors_GetPagedByParameters_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AttendanceService.GetPagedByParameters(int, AttendanceParameters, bool, CancellationToken)"/>
	/// method.
	/// </summary>
	public static readonly ApiError GetPagedByParametersNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetPagedByParametersNotFound)}",
			RESX.AttendanceServiceErrors_GetPagedByParameters_NotFound);
}
