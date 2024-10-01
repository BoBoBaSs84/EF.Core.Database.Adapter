using System.ComponentModel.DataAnnotations;

using Application.Contracts.Requests.Attendance.Base;

namespace Application.Contracts.Requests.Attendance;

/// <summary>
/// The attendance update request class.
/// </summary>
public sealed class AttendanceUpdateRequest : AttendanceBaseRequest
{
	/// <summary>
	/// The globally unique identifier property.
	/// </summary>	
	[Required]
	public required Guid Id { get; init; }
}
