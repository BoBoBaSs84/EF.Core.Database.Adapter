﻿using Domain.Converters.JsonConverters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Contracts.Requests;

/// <summary>
/// The attendance create request class.
/// </summary>
public sealed class AttendanceCreateRequest
{
	/// <summary>
	/// The <see cref="CalendarDayId"/> property.
	/// </summary>
	[Required, Range(0, int.MaxValue)]
	public int CalendarDayId { get; set; } = default!;

	/// <summary>
	/// The <see cref="DayTypeId"/> property.
	/// </summary>
	[Required, Range(0, int.MaxValue)]
	public int DayTypeId { get; set; } = default!;

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
