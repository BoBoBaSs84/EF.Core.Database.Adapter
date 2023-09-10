using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Application.Contracts.Responses.Base;

using Domain.Converters.JsonConverters;
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
	[DataType(DataType.Date)]
	public DateTime Date { get; set; }

	/// <summary>
	/// The attendance type property.
	/// </summary>
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public AttendanceType AttendanceType { get; set; }

	/// <summary>
	/// The start time property.
	/// </summary>
	[JsonConverter(typeof(TimeSpanJsonConverter))]
	public TimeSpan? StartTime { get; set; }

	/// <summary>
	/// The end time property.
	/// </summary>
	[JsonConverter(typeof(TimeSpanJsonConverter))]
	public TimeSpan? EndTime { get; set; }

	/// <summary>
	/// The break time property.
	/// </summary>
	[JsonConverter(typeof(TimeSpanJsonConverter))]
	public TimeSpan? BreakTime { get; set; }
}
