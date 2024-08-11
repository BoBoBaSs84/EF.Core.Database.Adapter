using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Domain.Converters;
using Domain.Enumerators.Attendance;

namespace Application.Contracts.Requests.Attendance;

/// <summary>
/// The attendance create request class.
/// </summary>
public sealed class AttendanceCreateRequest
{
	/// <summary>
	/// The date property.
	/// </summary>
	[Required]
	[DataType(DataType.Date)]
	[JsonConverter(typeof(DateJsonConverter))]
	public DateTime Date { get; set; }

	/// <summary>
	/// The attendance type property.
	/// </summary>
	[Required]
	public AttendanceType Type { get; set; }

	/// <summary>
	/// The start time property.
	/// </summary>
	[DataType(DataType.Time)]
	[JsonConverter(typeof(NullableTimeJsonConverter))]
	public TimeSpan? StartTime { get; set; }

	/// <summary>
	/// The end time property.
	/// </summary>
	[DataType(DataType.Time)]
	[JsonConverter(typeof(NullableTimeJsonConverter))]
	public TimeSpan? EndTime { get; set; }

	/// <summary>
	/// The break time property.
	/// </summary>
	[DataType(DataType.Time)]
	[JsonConverter(typeof(NullableTimeJsonConverter))]
	public TimeSpan? BreakTime { get; set; }
}
