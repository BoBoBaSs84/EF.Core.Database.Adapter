using System.ComponentModel.DataAnnotations;

using BB84.Home.Application.Contracts.Requests.Attendance.Base;

namespace BB84.Home.Application.Contracts.Requests.Attendance;

/// <summary>
/// The request for updating a attendance.
/// </summary>
public sealed class AttendanceUpdateRequest : AttendanceBaseRequest
{
	/// <summary>
	/// The unique identifier of the attendance.
	/// </summary>
	[Required]
	public required Guid Id { get; init; }
}
