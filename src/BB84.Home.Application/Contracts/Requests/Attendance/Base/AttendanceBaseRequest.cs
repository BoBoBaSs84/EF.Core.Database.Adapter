using System.ComponentModel.DataAnnotations;

using BB84.Home.Domain.Enumerators.Attendance;

namespace BB84.Home.Application.Contracts.Requests.Attendance.Base;

/// <summary>
/// The base request for creating or updating a attendance.
/// </summary>
public abstract class AttendanceBaseRequest
{
	/// <summary>
	/// The type of the attendance.
	/// </summary>
	[Required]	
	public required AttendanceType Type { get; init; }

	/// <summary>
	/// The start time of the attendance.
	/// </summary>
	[DataType(DataType.Time)]
	public TimeSpan? StartTime { get; init; }

	/// <summary>
	/// The end time of the attendance.
	/// </summary>
	[DataType(DataType.Time)]
	public TimeSpan? EndTime { get; init; }

	/// <summary>
	/// The break time of the attendance.
	/// </summary>
	[DataType(DataType.Time)]
	public TimeSpan? BreakTime { get; init; }
}
