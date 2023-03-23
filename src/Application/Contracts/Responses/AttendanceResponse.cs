using Application.Contracts.Responses.Base;
using Domain.Converters.JsonConverters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Contracts.Responses;

/// <summary>
/// The attendance response class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="ResponseModel"/> class.
/// </remarks>
public sealed class AttendanceResponse : ResponseModel
{
	/// <summary>
	/// The <see cref="Date"/> property.
	/// </summary>
	[DataType(DataType.Date)]
	public DateTime Date { get; set; } = default!;

	/// <summary>
	/// The <see cref="DayType"/> property.
	/// </summary>
	public string DayType { get; set; } = default!;

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

	/// <summary>
	/// The <see cref="DayTypeId"/> property.
	/// </summary>
	public int CalendarDayId { get; set; } = default!;

	/// <summary>
	/// The <see cref="DayTypeId"/> property.
	/// </summary>
	public int DayTypeId { get; set; } = default!;
}
