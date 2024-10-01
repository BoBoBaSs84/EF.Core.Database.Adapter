using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Application.Contracts.Requests.Attendance.Base;
using Application.Converters;

namespace Application.Contracts.Requests.Attendance;

/// <summary>
/// The attendance create request class.
/// </summary>
public sealed class AttendanceCreateRequest : AttendanceBaseRequest
{
	/// <summary>
	/// The date property.
	/// </summary>
	[Required]
	[JsonConverter(typeof(DateTimeJsonConverter))]
	public required DateTime Date { get; init; }
}
