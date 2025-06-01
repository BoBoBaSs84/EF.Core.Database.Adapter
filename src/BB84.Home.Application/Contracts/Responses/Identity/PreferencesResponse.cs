using System.Text.Json.Serialization;

using BB84.Home.Application.Converters;
using BB84.Home.Domain.Enumerators.Attendance;

namespace BB84.Home.Application.Contracts.Responses.Identity;

/// <summary>
/// The preferences response class.
/// </summary>
public sealed class PreferencesResponse
{
	/// <summary>
	/// The attendance preferences.
	/// </summary>
	public AttendancePreferencesResponse? AttendancePreferences { get; set; }
}

/// <summary>
/// The attendance preferences response class.
/// </summary>
public sealed class AttendancePreferencesResponse
{
	/// <summary>
	/// The work days per week property.
	/// </summary>
	[JsonConverter(typeof(FlagsJsonConverterFactory))]
	public WorkDayTypes WorkDays { get; set; }

	/// <summary>
	/// The work hours per week property.
	/// </summary>
	public float WorkHours { get; set; }

	/// <summary>
	/// The vacation days per year.
	/// </summary>
	public int VacationDays { get; set; }
}
