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
	/// The <see cref="Date"/> property.
	/// </summary>
	[DataType(DataType.Date)]
	public DateTime Date { get; set; } = default!;

	/// <summary>
	/// The day type property.
	/// </summary>
	public DayType DayType { get; set; } = default!;

	/// <summary>
	/// The <see cref="StartTime"/> property.
	/// </summary>
	[JsonConverter(typeof(TimeSpanJsonConverter))]
	public TimeSpan? StartTime { get; set; } = default!;

	/// <summary>
	/// The <see cref="EndTime"/> property.
	/// </summary>
	[JsonConverter(typeof(TimeSpanJsonConverter))]
	public TimeSpan? EndTime { get; set; } = default!;

	/// <summary>
	/// The <see cref="BreakTime"/> property.
	/// </summary>
	[JsonConverter(typeof(TimeSpanJsonConverter))]
	public TimeSpan? BreakTime { get; set; } = default!;
}
