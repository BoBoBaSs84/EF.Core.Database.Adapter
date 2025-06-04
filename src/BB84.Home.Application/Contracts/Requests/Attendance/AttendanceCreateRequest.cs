using System.ComponentModel.DataAnnotations;

using BB84.Home.Application.Contracts.Requests.Attendance.Base;

namespace BB84.Home.Application.Contracts.Requests.Attendance;

/// <summary>
/// The request for creating a attendance.
/// </summary>
public sealed class AttendanceCreateRequest : AttendanceBaseRequest
{
	/// <summary>
	/// The date of the attendance.
	/// </summary>
	[Required]
	[DataType(DataType.Date)]
	public required DateTime Date { get; init; }
}
