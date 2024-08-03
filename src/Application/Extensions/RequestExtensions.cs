using Application.Contracts.Requests.Attendance;

using Domain.Enumerators;
using Domain.Extensions;

namespace Application.Extensions;

/// <summary>
/// The request extensions class.
/// </summary>
public static class RequestExtensions
{
	/// <summary>
	/// Returns if the attendance create request is valid.
	/// </summary>
	/// <param name="request">The attendance create request to work with.</param>
	/// <returns><see langword="true"/> if the attendance create request is valid otherwise <see langword="false"/></returns>
	public static bool IsValid(this AttendanceCreateRequest request)
		=> IsAttendanceRequestValid(request.Type, request.StartTime, request.EndTime, request.BreakTime);

	/// <summary>
	/// Returns if the attendance update request is valid.
	/// </summary>
	/// <param name="request">The attendance update request to work with.</param>
	/// <returns><see langword="true"/> if the attendance update request is valid otherwise <see langword="false"/></returns>
	public static bool IsValid(this AttendanceUpdateRequest request)
		=> IsAttendanceRequestValid(request.Type, request.StartTime, request.EndTime, request.BreakTime);

	private static bool IsAttendanceRequestValid(AttendanceType type, TimeSpan? startTime, TimeSpan? endTime, TimeSpan? breakTime)
		=> (!type.IsWorkingHoursRelevant() || (startTime.HasValue && endTime.HasValue && !(endTime <= startTime)))
			&& (type.IsWorkingHoursRelevant() || (!startTime.HasValue && !endTime.HasValue && !breakTime.HasValue));
}
