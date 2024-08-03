using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Domain.Converters;
using Domain.Enumerators;

namespace Application.Contracts.Requests.Attendance;

/// <summary>
/// The attendance update request class.
/// </summary>
public sealed class AttendanceUpdateRequest
{
	/// <summary>
	/// The globally unique identifier property.
	/// </summary>	
	[Required]
	public Guid Id { get; set; }

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
