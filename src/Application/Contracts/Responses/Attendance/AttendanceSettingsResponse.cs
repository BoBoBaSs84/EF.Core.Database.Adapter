using System.Text.Json.Serialization;

using Application.Contracts.Responses.Base;

using Domain.Converters.JsonConverters;
using Domain.Enumerators;

namespace Application.Contracts.Responses.Attendance;

/// <summary>
/// The attendance settings response class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityResponse"/> class.
/// </remarks>
public sealed class AttendanceSettingsResponse : IdentityResponse
{
	/// <summary>
	/// The work days per week.
	/// </summary>
	[JsonConverter(typeof(FlagsToArrayJsonConverter))]
	public WorkDayTypes WorkDays { get; set; }

	/// <summary>
	/// The work hours per week.
	/// </summary>
	public float WorkHours { get; set; }
}
