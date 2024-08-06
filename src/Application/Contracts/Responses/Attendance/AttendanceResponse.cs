using System.Text.Json.Serialization;

using Application.Contracts.Responses.Base;

using Domain.Converters;
using Domain.Enumerators;

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
	[JsonConverter(typeof(DateJsonConverter))]
	public DateTime Date { get; set; }

	/// <summary>
	/// The attendance type property.
	/// </summary>
	public AttendanceType Type { get; set; }

	/// <summary>
	/// The start time property.
	/// </summary>	
	[JsonConverter(typeof(NullableTimeJsonConverter))]
	public TimeSpan? StartTime { get; set; }

	/// <summary>
	/// The end time property.
	/// </summary>
	[JsonConverter(typeof(NullableTimeJsonConverter))]
	public TimeSpan? EndTime { get; set; }

	/// <summary>
	/// The break time property.
	/// </summary>
	[JsonConverter(typeof(NullableTimeJsonConverter))]
	public TimeSpan? BreakTime { get; set; }

	/// <summary>
	/// The resulting working hours property.
	/// </summary>
	public float? WorkingHours { get; set; }
}
