using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Domain.Converters.JsonConverters;
using Domain.Enumerators;

namespace Application.Contracts.Requests.Attendance;

/// <summary>
/// The attendance update request class.
/// </summary>
public sealed class AttendanceUpdateRequest
{
	/// <summary>
	/// The identifier of the attendance.
	/// </summary>
	[Required]
	public Guid Id { get; set; }

	/// <summary>
	/// The data type property.
	/// </summary>
	[Required]
	public DayType DayType { get; set; }

	/// <summary>
	/// The <see cref="StartTime"/> property.
	/// </summary>
	[JsonConverter(typeof(TimeSpanJsonConverter))]
	public TimeSpan? StartTime { get; set; }

	/// <summary>
	/// The <see cref="EndTime"/> property.
	/// </summary>
	[JsonConverter(typeof(TimeSpanJsonConverter))]
	public TimeSpan? EndTime { get; set; }

	/// <summary>
	/// The <see cref="BreakTime"/> property.
	/// </summary>
	[JsonConverter(typeof(TimeSpanJsonConverter))]
	public TimeSpan? BreakTime { get; set; }
}
