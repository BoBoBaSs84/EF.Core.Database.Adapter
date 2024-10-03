using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Application.Contracts.Requests.Attendance.Base;
using Application.Converters;

namespace Application.Contracts.Requests.Attendance;

/// <summary>
/// The request for creating a attendance.
/// </summary>
public sealed class AttendanceCreateRequest : AttendanceBaseRequest
{
	/// <summary>
	/// The date of the attendance.
	/// </summary>
	[Required]
	[JsonConverter(typeof(DateTimeJsonConverter))]
	public required DateTime Date { get; init; }
}
