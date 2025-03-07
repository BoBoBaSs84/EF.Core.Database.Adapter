﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using BB84.Home.Application.Converters;
using BB84.Home.Domain.Enumerators.Attendance;

namespace BB84.Home.Application.Contracts.Requests.Attendance.Base;

/// <summary>
/// The base request for creating or updating a attendance.
/// </summary>
public abstract class AttendanceBaseRequest
{
	/// <summary>
	/// The type of the attendance.
	/// </summary>
	[Required]
	public required AttendanceType Type { get; init; }

	/// <summary>
	/// The start time of the attendance.
	/// </summary>
	[JsonConverter(typeof(NullableTimeSpanJsonConverter))]
	public TimeSpan? StartTime { get; init; }

	/// <summary>
	/// The end time of the attendance.
	/// </summary>
	[JsonConverter(typeof(NullableTimeSpanJsonConverter))]
	public TimeSpan? EndTime { get; init; }

	/// <summary>
	/// The break time of the attendance.
	/// </summary>
	[JsonConverter(typeof(NullableTimeSpanJsonConverter))]
	public TimeSpan? BreakTime { get; init; }
}
