using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Application.Converters;

using Domain.Enumerators.Attendance;

namespace Application.Contracts.Requests.Attendance.Base;

/// <summary>
/// The base attendance request for creation or update.
/// </summary>
public abstract class AttendanceBaseRequest
{
	/// <summary>
	/// The attendance type property.
	/// </summary>
	[Required]
	public required AttendanceType Type { get; init; }

	/// <summary>
	/// The start time property.
	/// </summary>
	[JsonConverter(typeof(NullableTimeSpanJsonConverter))]
	public TimeSpan? StartTime { get; init; }

	/// <summary>
	/// The end time property.
	/// </summary>
	[JsonConverter(typeof(NullableTimeSpanJsonConverter))]
	public TimeSpan? EndTime { get; init; }

	/// <summary>
	/// The break time property.
	/// </summary>
	[JsonConverter(typeof(NullableTimeSpanJsonConverter))]
	public TimeSpan? BreakTime { get; init; }
}
