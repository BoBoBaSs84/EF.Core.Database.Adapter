using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Application.Contracts.Responses.Base;
using Application.Converters;

using BB84.Home.Domain.Enumerators.Attendance;

namespace Application.Contracts.Responses.Attendance;

/// <summary>
/// The attendance response class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityResponse"/> class.
/// </remarks>
public sealed class AttendanceResponse : IdentityResponse
{
	/// <summary>
	/// The date property.
	/// </summary>
	[Required, DataType(DataType.Date)]
	[JsonConverter(typeof(DateTimeJsonConverter))]
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
	[JsonConverter(typeof(NullableTimeSpanJsonConverter))]
	public TimeSpan? StartTime { get; set; }

	/// <summary>
	/// The end time property.
	/// </summary>
	[DataType(DataType.Time)]
	[JsonConverter(typeof(NullableTimeSpanJsonConverter))]
	public TimeSpan? EndTime { get; set; }

	/// <summary>
	/// The break time property.
	/// </summary>
	[DataType(DataType.Time)]
	[JsonConverter(typeof(NullableTimeSpanJsonConverter))]
	public TimeSpan? BreakTime { get; set; }

	/// <summary>
	/// The resulting working hours property.
	/// </summary>
	public float? WorkingHours { get; set; }
}
